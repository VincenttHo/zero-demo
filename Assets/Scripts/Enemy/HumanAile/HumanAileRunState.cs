using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanAileRunState : BaseState
{

    private HumanAile humanAile;
    private GameObject player;
    private PlayerZero zero;

    public HumanAileRunState(HumanAile humanAile)
    {
        this.humanAile = humanAile;
        player = GameObject.FindGameObjectWithTag("Player");
        if(player != null)
        {
            zero = player.GetComponent<PlayerZero>();
        }
    }

    public override void execute()
    {

        if(!zero.isGrounded && Math.Abs(zero.transform.position.x - humanAile.transform.position.x) <= humanAile.jumpCheckXDistance)
        {
            humanAile.rigi.velocity = new Vector3(humanAile.runSpeed, humanAile.jumpForce, 0);
        }
        else
        {
            Vector3.MoveTowards(humanAile.transform.position, new Vector3(player.transform.position.x, 0, humanAile.transform.position.z), humanAile.runSpeed);
        }
        
        humanAile.anim.SetBool("isRunning", true);
    }

    public override bool onEndState()
    {
        return true;
    }


}
