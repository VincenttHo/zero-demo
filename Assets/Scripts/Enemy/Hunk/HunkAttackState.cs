using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HunkAttackState : BaseState
{

    private Hunk hunk;
    private float waitTime = 0;
    private float endWaitTime = 0;
    private GameObject player;

    public HunkAttackState(Hunk hunk)
    {
        this.hunk = hunk;
        endWaitTime = hunk.attackDelay;
        player = GameObject.FindGameObjectWithTag("Player");
        stateName = "attack";
    }

    public override void execute()
    {
        // 看向玩家
        LookAtPlayer();
        if(waitTime <= 0)
        {
            hunk.anim.SetTrigger("Attack");
            waitTime = hunk.attackWaitTime;
        } else
        {
            waitTime -= Time.deltaTime;
        }
    }

    private void LookAtPlayer()
    {
        if(player != null)
        {
            if(player.transform.position.x < hunk.transform.position.x)
            {
                hunk.LookLeft();
            } else
            {
                hunk.LookRight();
            }
        }
    }

    public override bool onEndState()
    {
        if(endWaitTime <= 0)
        {
            return true;
        } 
        else
        {
            endWaitTime -= Time.deltaTime;
            return false;
        }
    }

}
