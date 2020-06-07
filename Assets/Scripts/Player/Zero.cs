using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zero : Player
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
    // 是否可冲刺判断
    public bool canDash = true;
    // 碰墙判断
    public bool isTouchingWall;
    public bool slidingWall;
    public float wallSlidingSpeed;
    private bool wallJumping;
    public float xWallJumpSpeed;
    public float yWallJumpSpeed;
    public Transform wallCheck;
    public float checkRadius;
    public bool isTouchingOneWayPlatform;
    public float enableBodySpeed;
    public bool isTouchingLadder;
    public bool isClimbingLadder;
    public float climbLadderSpeed;
    private float climbLadderDir;
    private float defaultGravityScale;

    // 组件
    private BoxCollider2D myFeet;
    private CapsuleCollider2D myBody;
    private PlayerStateManager playerStateManager;
    public ShadowZero shadowZero;
    public int shadowMaxNum = 1;
    private int shadowCount = 0;
    public float initShadowDistance = 0.5f;
    private Transform dashStartPos;
    private GameObject[] shadowZeros;
    public bool isShadow = false;

    void Start()
    {
        base.Start();
        
        shadowZeros = GameObject.FindGameObjectsWithTag("ShadowZero");
        foreach (GameObject shadow in shadowZeros)
        {
            shadow.SetActive(false);
        }
        myFeet = GetComponent<BoxCollider2D>();
        myBody = GetComponent<CapsuleCollider2D>();
        playerStateManager = GetComponent<PlayerStateManager>();
        defaultGravityScale = rigi.gravityScale;
    } 

    void Update() 
    {
        //base.Update();
        if (!playerStateManager.isHurt)
        {
            Run();
            Dash();
            Jump();
            Flip();
            CheckGrounded();
            DoMove();
            Shoot();
            CheckWall();
            SilderWall();
            WallJump();
            CheckLadder();
            ClimbLadder();
            AnimationListener();


            if (rigi.velocity.x == 0 && rigi.velocity.y == 0 && !playerStateManager.isAttack)
            {
                playerStateManager.Stand();
            }
        }
        
    }

    // 转身
    void Flip()
    {
        if (isClimbingLadder) return;

        dir = 0;
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
            //transform.localScale = new Vector3(dir * Math.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            transform.rotation = Quaternion.Euler(0, dir == -1 ? 180 : 0, 0);
        }
    }

    public int GetDir()
    {
        return transform.rotation.y == 0 ? 1 : -1;
    }

    // 跑步方法
    void Run()
    {
        if (isClimbingLadder) return;
        horizontalSpeed = dir * runSpeed;
    }

    // 冲刺方法
    void Jump()
    {
        if (!OneWayPlatformJump())
        {
            if (Input.GetKeyDown(KeyCode.U) && !playerStateManager.isAttack && !slidingWall)
            {
                if (isGrounded || isClimbingLadder)
                {
                    EndClimbLadder();
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
    }

    void Dash()
    {
        if (isClimbingLadder) return;
        if (Input.GetKey(KeyCode.I) && isGrounded && canDash)
        { 
            /*if(shadowCount < shadowMaxNum)
            {
                var newShadow = Instantiate(shadowZero);
                newShadow.transform.SetParent(this.transform);
                newShadow.transform.localPosition = new Vector3(- (initShadowDistance * dir), 0, 0);
                shadowCount++;
            }*/
            if(!isShadow)
            {
                isShadow = true;
                enableShadow();
            }
            horizontalSpeed = GetDir() * dashSpeed;
        }
        if (Input.GetKeyUp(KeyCode.I))
        {
            isShadow = false;
            canDash = true;
        }
    }

    // 根据速度移动
    void DoMove()
    {
        if (isClimbingLadder) return;
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
                if (!Input.GetKey(KeyCode.I)) { 
                    canDash = true;
                }
                playerStateManager.Run();
            }
        }
    }

    void CheckGrounded()
    {
        isGrounded = myFeet.IsTouchingLayers(LayerMask.GetMask("Ground")) 
            || myFeet.IsTouchingLayers(LayerMask.GetMask("Wall"))
            || myFeet.IsTouchingLayers(LayerMask.GetMask("MovingPlatform"))
            || myFeet.IsTouchingLayers(LayerMask.GetMask("OneWayPlatform"));
        isTouchingOneWayPlatform = myFeet.IsTouchingLayers(LayerMask.GetMask("OneWayPlatform"));


    }

    void AnimationListener()
    {
        this.anim.SetFloat("HorizontalSpeed", Math.Abs(horizontalSpeed));
        this.anim.SetFloat("VerticalSpeed", rigi.velocity.y);
        this.anim.SetBool("isGrounded", isGrounded);
        anim.SetBool("isWall", slidingWall);
        anim.SetBool("isClimbingLadder", isClimbingLadder);
    }

    void Shoot()
    {
        if (isClimbingLadder) return;
        if (Input.GetKeyDown(KeyCode.H))
        {
            this.anim.SetTrigger("shoot");
        }
    }

    public void GetDamage(float damage)
    {
        if(canHurt)
        {
            base.GetDamage(damage);
            anim.SetTrigger("hurt");
            playerStateManager.isHurt = true;
        }
    }

    public void endHurt()
    {
        playerStateManager.isHurt = false;
    }

    public void endDash()
    {
        canDash = false;
    }

    private void enableShadow()
    {
        foreach (GameObject shadow in shadowZeros)
        {
            shadow.SetActive(true);
        }
    }

    void CheckWall()
    {
        //isTouchingWall = myBody.IsTouchingLayers(LayerMask.GetMask("Wall"));
        isTouchingWall = Physics2D.OverlapCircle(wallCheck.position, checkRadius, LayerMask.GetMask("Wall"));
    }

    void SilderWall()
    {
        if(isTouchingWall && !isGrounded && dir != 0)
        {
            rigi.velocity = new Vector2(rigi.velocity.x, Mathf.Clamp(rigi.velocity.y, -wallSlidingSpeed, float.MaxValue));
            //rigi.velocity = new Vector2(rigi.velocity.x, -wallSlidingSpeed);
            slidingWall = true;
            
        } else
        {
            slidingWall = false;
        }
        
    }

    void WallJump()
    {
        if(Input.GetKeyDown(KeyCode.U) && slidingWall)
        {
            rigi.velocity = new Vector2(xWallJumpSpeed * -dir, yWallJumpSpeed);
        }
        /*if(wallJumping)
        {
            rigi.velocity = new Vector2(xWallJumpSpeed * -dir, yWallJumpSpeed);
        }*/
    }

    bool OneWayPlatformJump()
    {
        if(isTouchingOneWayPlatform)
        {
            if(Input.GetKey(KeyCode.S) && Input.GetKeyDown(KeyCode.U))
            {
                myBody.enabled = false;
                Invoke("EnableBody", enableBodySpeed);
                return true;
            } else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    void EnableBody()
    {
        myBody.enabled = true;
    }

    void CheckLadder()
    {
        isTouchingLadder = myFeet.IsTouchingLayers(LayerMask.GetMask("Ladder"));
    }

    void ClimbLadder()
    {
        if(isTouchingLadder)
        {
            if(Input.GetKeyDown(KeyCode.W))
            {
                isClimbingLadder = true;
                rigi.gravityScale = 0;
                rigi.velocity = new Vector2(0, 0);
            }
        }
        if(isClimbingLadder)
        {
            if(!isTouchingLadder)
            {
                EndClimbLadder();
            }
            else
            {
                climbLadderDir = 0;
                if (Input.GetKey(KeyCode.W))
                {
                    anim.speed = 1;
                    climbLadderDir = 1;
                }
                else if (Input.GetKey(KeyCode.S))
                {
                    if (!isGrounded)
                    {
                        anim.speed = 1;
                        climbLadderDir = -1;
                    }
                    else
                    {
                        EndClimbLadder();
                    }
                }
                else
                {
                    anim.speed = 0;
                }
                rigi.velocity = new Vector2(rigi.velocity.x, climbLadderSpeed * climbLadderDir);
            }
        }
    }

    void EndClimbLadder()
    {
        isClimbingLadder = false;
        rigi.gravityScale = defaultGravityScale;
        anim.speed = 1;
    }

}
