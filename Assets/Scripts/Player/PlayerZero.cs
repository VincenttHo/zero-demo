using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerZero : Player
{

    [Header("运动参数")]
    // 跑步速度
    public float runSpeed = 5f;
    // 冲刺速度
    public float dashSpeed = 8f;
    // 跳跃速度
    public float jumpSpeed = 13f;
    // 当前水平速度
    //[HideInInspector]
    public float currentHorizontalSpeed;
    // 运动方向
    //[HideInInspector]
    public int moveDir;

    //[HideInInspector]
    public bool isGrounded = true;
    [HideInInspector]
    public Rigidbody2D rigi;
    [HideInInspector]
    public Animator anim;

    private BoxCollider2D myFeet;

    private PlayerStateMachine stateMachine;

    private void Start()
    {
        base.Start();
        stateMachine = new PlayerStateMachine(this);
        rigi = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        myFeet = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        CheckGrounded();
        Flip();
        DoRun();
        DoDash();
        DoMove();
        stateMachine.CheckChangeState();
        stateMachine.currentState.execute();
    }

    public int GetDir()
    {
        return transform.rotation.y == 0 ? 1 : -1;
    }

    public bool DoJump()
    {
        return Input.GetKeyDown(KeyCode.U);
    }

    private void Flip()
    {
        moveDir = 0;
        if (Input.GetKey(KeyCode.A))
        {
            moveDir = -1;
        }
        else if(Input.GetKey(KeyCode.D))
        {
            moveDir = 1;
        }
        if(moveDir != 0)
        {
            transform.rotation = Quaternion.Euler(0, moveDir == 1 ? 0 : 180, 0);
        }
    }

    private void DoRun()
    {
        currentHorizontalSpeed = runSpeed * moveDir;
    }

    private void DoDash()
    {
        if(Input.GetKey(KeyCode.I))
        {
            currentHorizontalSpeed = dashSpeed * GetDir();
        }
    }

    private void DoMove()
    {
        rigi.velocity = new Vector2(currentHorizontalSpeed, rigi.velocity.y);
    }

    private void CheckGrounded()
    {
        isGrounded = myFeet.IsTouchingLayers(LayerMask.GetMask("Ground"));

    }

}
