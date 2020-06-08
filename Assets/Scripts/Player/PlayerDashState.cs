using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : BaseState
{

    private PlayerZero playerZero;

    public PlayerDashState(PlayerZero playerZero)
    {
        this.playerZero = playerZero;
        stateName = "dash";
        enableShadow();
    }

    public override void execute()
    {
        playerZero.rigi.velocity = new Vector2(playerZero.currentHorizontalSpeed, playerZero.rigi.velocity.y);
        playerZero.anim.SetBool("isDash", true);
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
