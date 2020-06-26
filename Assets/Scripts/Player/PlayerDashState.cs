using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : BaseState
{

    private PlayerZero playerZero;
    private bool canAudio;

    public PlayerDashState(PlayerZero playerZero)
    {
        this.playerZero = playerZero;
        stateName = "dash";
        canAudio = true;
        //enableShadow();
    }

    public override void execute()
    {
        if(canAudio)
        {
            SoundManager.PlayAudio(SoundManager.dash);
            canAudio = false;
        }
        
        playerZero.rigi.velocity = new Vector2(playerZero.currentHorizontalSpeed, playerZero.rigi.velocity.y);
        playerZero.anim.SetBool("isDash", true);
        ShadowObjectPool.instance.GetShadow();
    }

    public override bool onEndState()
    {
        playerZero.rigi.velocity = new Vector2(0, playerZero.rigi.velocity.y);
        playerZero.anim.SetBool("isDash", false);
        return true;
    }

    private void enableShadow()
    {
        foreach (GameObject shadow in playerZero.shadowZeros)
        {
            shadow.SetActive(true);
        }
    }
}
