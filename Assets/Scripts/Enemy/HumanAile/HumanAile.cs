

using System;
using UnityEngine;

public class HumanAile : Boss
{

    private GameObject player;
    [HideInInspector]
    public Rigidbody2D rigi;

    public float runSpeed;
    public float lv1RunSpeed;
    public float lv2RunSpeed;
    public float jumpForce;
    public float jumpCheckXDistance;
    private PlayerZero zero;
    public bool isGrounded;
    private CapsuleCollider2D myFeet;
    private bool canJump;

    public Transform leftMovePos;
    public Transform rightMovePos;
    private Transform movePos;
    public float moveWaitSec;
    public float moveWaitDuration;

    public float hp;
    private bool isDead;

    void Start()
    {
        base.Start();
        AileHpManager.maxHp = hp;
        AileHpManager.currentHp = hp;
        movePos = rightMovePos;
        moveWaitDuration = moveWaitSec;
        player = GameObject.FindGameObjectWithTag("Player");
        if(player != null)
        {
            zero = player.GetComponent<PlayerZero>();
        }
        rigi = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        myFeet = GetComponent<CapsuleCollider2D>();
    }

    void Update()
    {
        if (isDead) return;
        if (!canMove) return;
        if (player == null) return;
        if (AileHpManager.currentHp <= 0)
        {
            isDead = true;
            rigi.velocity = new Vector2(0, 0);
            HumanAileAudioManager.instance.PlayAudio(HumanAileAudioManager.deadAudio);
            BgmManager.StopBgm();
            anim.SetTrigger("dead");
            TimelineManager.instance.PlayBossChangeStory();
            //GameController.instance.ScreenChange();
            return;
        }
        CheckGround();
        DoFilp();
        DoRun();
        DoJump();
        anim.SetFloat("verticalSpeed", rigi.velocity.y);
        anim.SetBool("isGrounded", isGrounded);
    }

    private void DoFilp()
    {
        /*if (player.transform.position.x < transform.position.x)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (player.transform.position.x > transform.position.x)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }*/
        if (Math.Abs(transform.position.x - movePos.position.x) > 0.1f)
        {
            if (transform.position.x > movePos.position.x)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            } 
            else
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
        }
    }

    private void DoRun()
    {
        /*if (zero.isGrounded && Math.Abs(zero.transform.position.x - transform.position.x) > 0.1)
        {
            rigi.velocity = new Vector3(runSpeed * -transform.right.x, rigi.velocity.y, 0);
        }*/
        if(Math.Abs(transform.position.x - leftMovePos.position.x) <= 0.2f)
        {
            movePos = rightMovePos;
        }
        else if(Math.Abs(transform.position.x - rightMovePos.position.x) <= 0.2f)
        {
            movePos = leftMovePos;
            if(moveWaitDuration > 0)
            {
                anim.SetBool("isRunning", false);
                moveWaitDuration -= Time.deltaTime;
                runSpeed = 0;
            } 
            else
            {
                anim.SetBool("isRunning", true);
                moveWaitDuration = moveWaitSec;
                if(AileHpManager.currentHp <= (0.5 * AileHpManager.maxHp))
                {
                    runSpeed = lv2RunSpeed;
                }
                else
                {
                    runSpeed = lv1RunSpeed;
                }
            }
        }

        rigi.velocity = new Vector3(runSpeed * -transform.right.x, rigi.velocity.y, 0);

    }

    private void DoJump()
    {
        if(isGrounded)
        {
            canJump = true;
        }
        if (!zero.isGrounded && Math.Abs(zero.transform.position.x - transform.position.x) <= jumpCheckXDistance)
        {
            if(canJump)
            {
                rigi.velocity = new Vector3(runSpeed * -transform.right.x, jumpForce, 0);
                canJump = false;
            }
        }
    }

    private void CheckGround()
    {
        isGrounded = myFeet.IsTouchingLayers(LayerMask.GetMask("Ground"))
            || myFeet.IsTouchingLayers(LayerMask.GetMask("Wall"))
            || myFeet.IsTouchingLayers(LayerMask.GetMask("MovingPlatform"))
            || myFeet.IsTouchingLayers(LayerMask.GetMask("OneWayPlatform"));
    }

    private void OnDisable()
    {
        AileHpManager.maxHp = 60;
        AileHpManager.currentHp = 60;
    }

}
