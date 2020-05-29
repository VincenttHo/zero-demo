using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// zero移动脚本
/// </summary>
public class Player : MonoBehaviour 
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
    private bool isGrounded;
    // 二段跳判断
    public bool canDoubleJump;

    // 组件
    private Rigidbody2D rigi;
    private Animator anim; public Transform grandCheck;
    public LayerMask GroundLayer;
    private BoxCollider2D myFeet;

    void Start()
    {
        rigi = GetComponentInChildren<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        myFeet = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        Run();
        Dash();
        Jump();
        Flip();
        CheckGrounded();
        AnimationListener();
        DoMove();
    }

    void Flip()
    {
        dir = 0;
        if (Input.GetKey(KeyCode.A))
        {
            dir = -1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            dir = 1;
        }
        if(dir != 0)
        {
            transform.localScale = new Vector3(dir, 1, 1);
        }
    }

    // 跑步方法
    void Run()
    {
        horizontalSpeed = dir * runSpeed;
    }

    // 冲刺方法
    void Jump()
    {
        if(Input.GetKeyDown(KeyCode.U))
        {
            if(isGrounded)
            {
                rigi.velocity = new Vector2(rigi.velocity.x, jumpSpeed);
                canDoubleJump = true;
                //this.rigi.AddForce(new Vector2(0, jumpSpeed));
            }
            else
            {
                if(canDoubleJump)
                {
                    rigi.velocity = new Vector2(rigi.velocity.x, doubleJumpSpeed);
                    canDoubleJump = false;
                }
            }
        }
    }

    void Dash()
    {
        if (Input.GetKey(KeyCode.I) && isGrounded)
        {
            horizontalSpeed = this.transform.localScale.x * dashSpeed;
        }
    }

    // 根据速度移动
    void DoMove()
    {
        rigi.velocity = new Vector2(horizontalSpeed, rigi.velocity.y);
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

}
