

using System;
using UnityEngine;

public class HumanAile : Enemy
{

    private GameObject player;
    [HideInInspector]
    public Rigidbody2D rigi;
    [HideInInspector]
    public Animator anim;

    public float runSpeed;
    public float jumpForce;
    public float jumpCheckXDistance;
    private PlayerZero zero;
    public bool isGrounded;
    private CapsuleCollider2D myFeet;
    private bool canJump;


    void Start()
    {
        base.Start();
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
        if (player == null) return;
        CheckGround();
        DoFilp();
        //DoRun();
       // DoJump();
        anim.SetFloat("verticalSpeed", rigi.velocity.y);
        anim.SetBool("isGrounded", isGrounded);
    }

    private void DoFilp()
    {
        
        if (player.transform.position.x < transform.position.x)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (player.transform.position.x > transform.position.x)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }

    private void DoRun()
    {
        if (zero.isGrounded && Math.Abs(zero.transform.position.x - transform.position.x) > 0.1)
        {
            rigi.velocity = new Vector3(runSpeed * -transform.right.x, rigi.velocity.y, 0);
        }
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

}
