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
    // 冲刺开关
    [HideInInspector]
    public bool canDash = true;
    // 当前水平速度
    //[HideInInspector]
    public float currentHorizontalSpeed;
    // 运动方向
   // [HideInInspector]
    public int input;
    // 跳跃次数
    public int jumpCount;
    // 最大跳跃次数
    public int maxJumpTime = 2;
    // 纵向输入
    public int yInput;
    // 滑墙速度
    public float wallSlidingSpeed;
    // 跳墙速度
    public float wallJumpYSpeed;
    public float wallJumpXSpeed;
    // 跳墙cd
    public float wallJumpCD;
    public float wallJumpWaitTime;
    //[HideInInspector]
    public bool canJump = true;
    public bool isHurt = false;

    /**判断参数*/
    //[HideInInspector]
    public bool isGrounded = true;
    public bool isAttack = false;
    public bool isTouchingWall = false;
    public bool isDashJump = false;

    public string currentState;

    private CapsuleCollider2D myFeet;

    public Transform wallCheck;
    public float checkRadius;
    [HideInInspector]
    public PlayerStateMachine stateMachine;
    [HideInInspector]
    public GameObject[] shadowZeros;

    public bool canControll;

    public GameObject playerDeadEffect;

    private Vector3 testDir;

    private void Start()
    {
        base.Start();
        stateMachine = new PlayerStateMachine(this);
        rigi = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        myFeet = GetComponent<CapsuleCollider2D>();
        shadowZeros = GameObject.FindGameObjectsWithTag("ShadowZero");
        foreach (GameObject shadow in shadowZeros)
        {
            shadow.SetActive(false);
        }
        //canControll = false;
        testDir = transform.up;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            print(AileHpManager.currentHp);
        }
        if (canControll)
        {
            anim.SetFloat("verticalSpeed", rigi.velocity.y);
            currentState = stateMachine.currentState.stateName;
            CheckWall();
            CheckGrounded();
            DoInput();
            DoRun();
            DoDash();
            DoJump();
            Shoot();
            stateMachine.CheckChangeState();
            stateMachine.currentState.execute();
        }
    }

    public int GetDir()
    {
        return transform.rotation.y == 0 ? 1 : -1;
    }

    public void DoJump()
    {
        if(Input.GetKey(KeyCode.U) && Input.GetKey(KeyCode.I))
        {
            isDashJump = true;
        }
        if(Input.GetKey(KeyCode.U))
        {
             yInput = 1;
        }
        else if(Input.GetKeyUp(KeyCode.U))
        {
            canJump = true;
            yInput = 0;
        }
    }

    private void DoInput()
    {
        if (isAttack) return;
        input = 0;
        if (Input.GetKey(KeyCode.A))
        {
            input = -1;
        }
        else if(Input.GetKey(KeyCode.D))
        {
            input = 1;
        }
        if(input != 0 && !isHurt)
        {
            transform.rotation = Quaternion.Euler(0, input == 1 ? 0 : 180, 0);
        }
    }

    private void DoRun()
    {
        currentHorizontalSpeed = runSpeed * input;
    }

    private void DoDash()
    {
        if(Input.GetKey(KeyCode.I) && canDash)
        {
            currentHorizontalSpeed = dashSpeed * GetDir();
        }
        if(Input.GetKeyUp(KeyCode.I))
        {
            canDash = true;
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

    public void EndDash()
    {
        canDash = false;
    }

    public void EndJump()
    {
        if(yInput == 1)
        {
            canJump = false;
        }
    }

    void Shoot()
    {
        //if (isClimbingLadder) return;
        if (Input.GetKeyDown(KeyCode.H))
        {
            this.anim.SetTrigger("shoot");
        }
    }

    void CheckWall()
    {
        isTouchingWall = Physics2D.OverlapCircle(wallCheck.position, checkRadius, LayerMask.GetMask("Ground"));
    }

    public void GetDamage(float damage)
    {
        base.GetDamage(damage);
        isHurt = true;
        anim.SetTrigger("hurt");
    }

    public void EndHurt()
    {
        isHurt = false;
        rigi.velocity = new Vector2(0, rigi.velocity.y);
    }

    public void EnableControll()
    {
        canControll = true;
    }

}
