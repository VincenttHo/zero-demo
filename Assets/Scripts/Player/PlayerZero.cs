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

    private bool isShoot;

    private Vector3 testDir;

    // 踏步秒数
    public float stepSec;

    public float gunChargeSec;
    private float gunChargeWaitSec;
    public int gunChargeLv;
    private GunChargeController gunChargeController;


    public GameObject bullet;
    public GameObject lv1Bullet;
    public GameObject lv2Bullet;
    public Transform bulletPos;

    private Transform playerDefPos;

    public bool isPlatformJump;
    

    private void Start()
    {

        base.Start();
        playerDefPos = transform.parent;
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
        gunChargeWaitSec = gunChargeSec;
        gunChargeController = GetComponentInChildren<GunChargeController>();
    }

    void FixedUpdate()
    {
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
        if (rigi.velocity.y < -45)
        {
            rigi.velocity = new Vector2(rigi.velocity.x, -45);
        }

        /*if (isTouchingPlatform() && PlayerController.instance.inputDown && PlayerController.instance.jump)
        {
            GetComponent<BoxCollider2D>().enabled = false;
            //myFeet.enabled = false;
            transform.parent = playerDefPos;
            StartCoroutine(ResetBoxCollider());
            
        }*/

    }

    IEnumerator ResetBoxCollider()
    {
        yield return new WaitForSeconds(0.2f);
        GetComponent<BoxCollider2D>().enabled = true;
        //myFeet.enabled = true;
    }

    public int GetDir()
    {
        return transform.rotation.y == 0 ? 1 : -1;
    }

    public void DoJump()
    {
        if (isGrounded || stateMachine.currentState is PlayerSlideWallState)
        {
            isDashJump = false;
        }

        //if(Input.GetKey(KeyCode.U) && Input.GetKey(KeyCode.I))
        if (PlayerController.instance.jump && PlayerController.instance.dash)
        {
            if (isGrounded 
                || stateMachine.currentState is PlayerSlideWallState 
                || stateMachine.currentState.lastState is PlayerSlideWallState)
            {
                isDashJump = true;
            }
        }
        //if(Input.GetKey(KeyCode.U))
        if (PlayerController.instance.jump)
        {
            if(transform.parent != playerDefPos)
            {
                isPlatformJump = true;
            } 
            else
            {
                isPlatformJump = false;
            }
            yInput = 1;
        }
        //else if(Input.GetKeyUp(KeyCode.U))
        else if (!PlayerController.instance.jump)
        {
            canJump = true;
            yInput = 0;
            if (transform.parent == playerDefPos)
            {
                isPlatformJump = false;
            }
        }
    }

    private void DoInput()
    {
        if (isAttack) return;
        if (isShoot) return;
        if (!GameController.instance.canControll) return;
        input = 0;
        //if (Input.GetKey(KeyCode.A))
        if (PlayerController.instance.inputLeft)
        {
            input = -1;
        }
        //else if(Input.GetKey(KeyCode.D))
        else if (PlayerController.instance.inputRight)
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
        //if(Input.GetKey(KeyCode.I) && canDash)
        if(PlayerController.instance.dash && canDash)
        {
            currentHorizontalSpeed = dashSpeed * GetDir();
        }
        //if(Input.GetKeyUp(KeyCode.I))
        if (!PlayerController.instance.dash)
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
            isPlatformJump = false;
        }
    }

    void Shoot()
    {
        //if (isClimbingLadder) return;
        //if (Input.GetKeyDown(KeyCode.H))
        if(PlayerController.instance.gunCharge)
        {
            if(gunChargeWaitSec <= 0)
            {
                gunChargeLv = gunChargeLv < 2 ? gunChargeLv + 1 : gunChargeLv;
                gunChargeWaitSec = gunChargeSec;
            }
            else
            {
                gunChargeWaitSec -= Time.deltaTime;
            }
        }
        if(PlayerController.instance.shoot)
        {
            gunChargeWaitSec = gunChargeSec;
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

    public void StartGame()
    {
        GameController.instance.StartGame();
    }

    void InitBullet()
    {
        //bullet.transform.position = bulletPos.position;
        //float rotationY = transform.localScale.x < 0 ? 0 : 180;
        //bullet.transform.rotation = Quaternion.Euler(0, rotationY, 0);
        SoundManager.PlayAudio(SoundManager.shoot);
        var initBullet = bullet;
        if (gunChargeLv == 1)
        {
            initBullet = lv1Bullet;
        }
        else if (gunChargeLv == 2)
        {
            initBullet = lv2Bullet;
        }
        GameObject newBullet = Instantiate(initBullet);
        newBullet.transform.position = bulletPos.position;
        newBullet.transform.rotation = transform.rotation;
        gunChargeLv = 0;
    }

    public bool isTouchingPlatform()
    {
        return myFeet.IsTouchingLayers(LayerMask.GetMask("MovingPlatform")) || myFeet.IsTouchingLayers(LayerMask.GetMask("OneWayPlatform"));
    }

}
