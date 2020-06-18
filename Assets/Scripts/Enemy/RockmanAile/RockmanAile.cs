

using System;
using UnityEditor.Build.Reporting;
using UnityEngine;

/**
 * 洛克人状态艾尔
 * 1.ZX状态：射击三发蓄力弹，在一定距离内会冲刺到玩家面前三连斩
 * 2.PX状态：发射一个可以在墙上多次反弹的风魔手里剑
 * 3.HX状态：在跟前放出一个有吸力的龙卷风（hp过半以后用，作为辅助技能，放了这个之后切换PX或者ZX攻击）
 * 4.LX状态：在自己的一侧放出一列冰（中间随机留一个缺口，缺口位置放一个跳台用于躲招）
 */
public class RockmanAile : Boss
{

    public RockmanAileController controller;
    public RockmanAileController.Model modelName;
    public bool canAction;
    public bool isGrounded;

    [HideInInspector]
    public CapsuleCollider2D myFeet;

    [HideInInspector]
    public Rigidbody2D rigi;

    [HideInInspector]
    public Animator anim;

    protected float step;

    public float dashSpeed;

    public Transform leftMovePos;
    public Transform rightMovePos;
    private Transform movePos;

    public GameObject player;

    public GameObject nextModel;

    protected void Start()
    {
        step = 1;
        controller = GetComponentInParent<RockmanAileController>();
        anim = GetComponent<Animator>();
        myFeet = GetComponent<CapsuleCollider2D>();
        rigi = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    protected void Update()
    {
        CheckGrounded();
    }

    public void EnableAction()
    {
        this.canAction = true;
    }

    public void LookRight()
    {
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    public void LookLeft()
    {
        transform.rotation = Quaternion.Euler(0, 180, 0);
    }

    private void CheckGrounded()
    {
        isGrounded = myFeet.IsTouchingLayers(LayerMask.GetMask("Ground"))
            || myFeet.IsTouchingLayers(LayerMask.GetMask("Wall"))
            || myFeet.IsTouchingLayers(LayerMask.GetMask("MovingPlatform"))
            || myFeet.IsTouchingLayers(LayerMask.GetMask("OneWayPlatform"));
    }

    protected void OnEnable()
    {
        step = 1;
    }

    public void Dash()
    {
        anim.SetBool("isDashing", true);
        rigi.velocity = new Vector2(dashSpeed * transform.right.x, rigi.velocity.y);
    }

    public void EndDash()
    {
        anim.SetBool("isDashing", false);
        rigi.velocity = new Vector2(0, rigi.velocity.y);
    }

    public void Step1Move()
    {
        if (step == 1)
        {
            if (player.transform.position.x < 0)
            {
                movePos = rightMovePos;
                if (rightMovePos.position.x > transform.position.x)
                {
                    LookRight();
                }
                else
                {
                    LookLeft();
                }
                Dash();
            }
            if (player.transform.position.x >= 0)
            {
                movePos = leftMovePos;
                if (leftMovePos.position.x > transform.position.x)
                {
                    LookRight();
                }
                else
                {
                    LookLeft();
                }
                Dash();
            }
        }

        if (step == 1 && movePos != null && Vector2.Distance(transform.position, movePos.position) <= 0.5f)
        {
            step = 2;
            EndDash();
            if (transform.position.x > 0)
            {
                LookLeft();
            }
            else
            {
                LookRight();
            }
        }
    }

    public void DoChange(GameObject nextModel)
    {
        this.anim.SetTrigger("turnOut");
        this.nextModel = nextModel;
        nextModel.transform.position = transform.position;
    }

    public void EndChange()
    {
        nextModel.SetActive(true);
        gameObject.SetActive(false);
    }

}
