

using System;
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

    private Animator anim;
    public GameObject bullet;
    public Transform bulletPos;
    public float step;

    public float dashSpeed;
    private Rigidbody2D rigi;

    private GameObject player;

    public Transform leftMovePos;
    public Transform rightMovePos;
    private Transform movePos;

    public float attackDistance;

    private PlayerZero zero;

    private bool isDashAttacking;
    private bool isJumpAttacking;

    public float jumpForce;
    private bool canJump;

    private CapsuleCollider2D myFeet;

    public bool isGrounded;

    void Start()
    {
        myFeet = GetComponent<CapsuleCollider2D>();
        canJump = true;
        step = 1;
        anim = GetComponent<Animator>();
        rigi = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            zero = player.GetComponent<PlayerZero>();
        }
    }

    void Update()
    {
        if (player == null) return;
        if(Input.GetKeyDown(KeyCode.Q) && step == 1)
        {
            if(player.transform.position.x < 0)
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

        if(step == 2)
        {
            Shoot();
            step = 2.5f;
        }

        if(step == 3)
        {
            if(Math.Abs(player.transform.position.x - transform.position.x) > attackDistance)
            {
                Dash();
            }
            else
            {
                EndDash();
                if(zero.isGrounded)
                {
                    isDashAttacking = true;
                    step = 3.5f;
                }
                else
                {
                    isJumpAttacking = true;
                    step = 3.5f;
                }
            }
        }

        DashAttack();
        JumpAttack();
        CheckGrounded();

    }

    void Shoot()
    {
        anim.SetTrigger("shoot");
    }

    void Dash()
    {
        anim.SetBool("isDashing", true);
        rigi.velocity = new Vector2(dashSpeed * transform.right.x, rigi.velocity.y);
    }

    void EndDash()
    {
        anim.SetBool("isDashing", false);
        rigi.velocity = new Vector2(0, rigi.velocity.y);
    }

    public void InitBullet()
    {
        var newBullet = GameObject.Instantiate(bullet);
        newBullet.transform.position = bulletPos.position;
        newBullet.transform.rotation = bulletPos.rotation;
    }

    private void LookRight()
    {
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    private void LookLeft()
    {
        transform.rotation = Quaternion.Euler(0, 180, 0);
    }

    public void EndShoot()
    {
        step = 3;
    }

    void DashAttack()
    {
        if(isDashAttacking)
        {
            anim.SetBool("isDashAttacking", true);
            rigi.velocity = new Vector2(dashSpeed * transform.right.x, rigi.velocity.y);
        }
    }

    public void EndDashAttack()
    {
        isDashAttacking = false;
        anim.SetBool("isDashAttacking", false);
        rigi.velocity = new Vector2(0, rigi.velocity.y);
        step = 4;
    }

    void JumpAttack()
    {
        if(isJumpAttacking)
        {
            anim.SetBool("isJumpAttacking", true);
            if(canJump)
            {
                rigi.velocity = new Vector2(dashSpeed * transform.right.x, jumpForce);
                canJump = false;
            }
            rigi.velocity = new Vector2(dashSpeed * transform.right.x, rigi.velocity.y);
            if(isGrounded)
            {
                anim.SetBool("isJumpAttacking", false);
                rigi.velocity = new Vector2(0, rigi.velocity.y);
            }
        }
    }

    private void CheckGrounded()
    {
        isGrounded = myFeet.IsTouchingLayers(LayerMask.GetMask("Ground"))
            || myFeet.IsTouchingLayers(LayerMask.GetMask("Wall"))
            || myFeet.IsTouchingLayers(LayerMask.GetMask("MovingPlatform"))
            || myFeet.IsTouchingLayers(LayerMask.GetMask("OneWayPlatform"));
        anim.SetBool("isGrounded", isGrounded);
    }

}
