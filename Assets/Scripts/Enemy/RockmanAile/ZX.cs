

using System;
using UnityEngine;

/**
 * 洛克人状态艾尔
 * 1.ZX状态：射击三发蓄力弹，在一定距离内会冲刺到玩家面前三连斩
 * 2.PX状态：发射一个可以在墙上多次反弹的风魔手里剑
 * 3.HX状态：在跟前放出一个有吸力的龙卷风（hp过半以后用，作为辅助技能，放了这个之后切换PX或者ZX攻击）
 * 4.LX状态：在自己的一侧放出一列冰（中间随机留一个缺口，缺口位置放一个跳台用于躲招）
 */
public class ZX : RockmanAile
{

    private RockmanAileController controller;

    public GameObject bullet;
    public Transform bulletPos;

    private GameObject player;

    public float attackDistance;

    private PlayerZero zero;

    private bool isDashAttacking;
    private bool isJumpAttacking;

    public float jumpForce;
    private bool canJump;

    void Start()
    {
        base.Start();
        modelName = RockmanAileController.Model.ZX;
        controller = GetComponentInParent<RockmanAileController>();
        canJump = true;
        player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            zero = player.GetComponent<PlayerZero>();
        }
        
    }

    void Update()
    {
        base.Update();
        if (player == null) return;
        if (!canAction) return;
        
        Step1Move();

        if (step == 2)
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

        if(step == 4)
        {
            //step = 1;
            /*if (AileHpManager.currentHp <= AileHpManager.maxHp / 2)
            {*/
                canAction = false;
                controller.ChangeModel();
            //}
        }

        DashAttack();
        JumpAttack();

    }

    void Shoot()
    {
        anim.SetTrigger("shoot");
    }

    public void InitBullet()
    {
        var newBullet = GameObject.Instantiate(bullet);
        newBullet.transform.position = bulletPos.position;
        newBullet.transform.rotation = bulletPos.rotation;
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
            if(rigi.velocity.y <= 0 && isGrounded)
            {
                canJump = true;
                isJumpAttacking = false;
                anim.SetBool("isJumpAttacking", false);
                rigi.velocity = new Vector2(0, rigi.velocity.y);
                step = 4;
            }
        }
    }

}
