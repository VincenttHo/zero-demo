using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowZero : Player
{
    /** 运动参数 **/
    // 跑步速度
    public float runSpeed = 5;
    // 冲刺速度
    public float dashSpeed = 8;
    // 实际角色左右运动速度
    private float horizontalSpeed = 0;
    // 角色运动方向 
    private float dir = 0;
    // 跳跃速度
    public float jumpSpeed = 13;
    // 二段跳速度
    public float doubleJumpSpeed = 13;
    // 着地判断
    public bool isGrounded;
    // 二段跳判断
    public bool canDoubleJump;
    // 开始位置
    private float startX;

    // 组件
    private BoxCollider2D myFeet;
    private PlayerStateManager playerStateManager;
    private PlayerZero zero;

    void Start()
    {
        base.Start();
        startX = transform.localPosition.x;
        myFeet = GetComponent<BoxCollider2D>();
        playerStateManager = GetComponent<PlayerStateManager>();
        zero = GetComponentInParent<PlayerZero>();
    }

    void Update()
    {
        if(Math.Abs(transform.localPosition.x) <= 0.1)
        {
            this.gameObject.SetActive(false);
            transform.localPosition = new Vector3(startX, 0, 0);
        }

        //base.Update();
        if (!playerStateManager.isHurt)
        {
            print(zero.rigi.velocity.y);

            rigi.velocity = new Vector3(rigi.velocity.x, zero.rigi.velocity.y);
            //Run();
            Dash();
            //Jump();
            Flip();
            CheckGrounded();
            AnimationListener();
            DoMove();
            if (rigi.velocity.x == 0 && rigi.velocity.y == 0 && !playerStateManager.isAttack)
            {
                playerStateManager.Stand();
            }
        }

    }

    // 转身
    void Flip()
    {
        /*dir = 0;
        if (Input.GetKey(KeyCode.A))
        {
            dir = -1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            dir = 1;
        }
        if (dir != 0)
        {
            transform.localScale = new Vector3(dir * Math.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }*/
    }

    // 跑步方法
    void Run()
    {
        horizontalSpeed = dir * runSpeed;
    }

    // 冲刺方法
    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.U) && !playerStateManager.isAttack)
        {
            if (isGrounded)
            {
                rigi.velocity = new Vector2(rigi.velocity.x, jumpSpeed);
                canDoubleJump = true;
                playerStateManager.Jump();
                //this.rigi.AddForce(new Vector2(0, jumpSpeed));
            }
            else
            {
                if (canDoubleJump)
                {
                    rigi.velocity = new Vector2(rigi.velocity.x, doubleJumpSpeed);
                    canDoubleJump = false;
                    playerStateManager.Jump();
                }
            }
        }
    }

    void Dash()
    {
        //if (Input.GetKey(KeyCode.I) && isGrounded)
        //{
            horizontalSpeed = (transform.parent.position.x > transform.position.x ? 1 : -1) * dashSpeed;

        //}
    }

    // 根据速度移动
    void DoMove()
    {
        if (!playerStateManager.isAttack)
        {
            rigi.velocity = new Vector2(horizontalSpeed, rigi.velocity.y);
            // 修改运动状态标识
            if (Math.Abs(horizontalSpeed) > runSpeed)
            {
                playerStateManager.Dash();
            }
            else if (Math.Abs(horizontalSpeed) > 0)
            {
                playerStateManager.Run();
            }
        }
    }

    void CheckGrounded()
    {
        isGrounded = myFeet.IsTouchingLayers(LayerMask.GetMask("Ground"));

    }

    void AnimationListener()
    {
        this.anim.SetFloat("HorizontalSpeed", Math.Abs(horizontalSpeed));
        this.anim.SetFloat("VerticalSpeed", rigi.velocity.y);
        this.anim.SetBool("isGrounded", isGrounded);
    }

    public void EndDash()
    {
    }

}
