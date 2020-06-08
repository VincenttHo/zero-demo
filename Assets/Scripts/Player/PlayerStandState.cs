using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStandState : BaseState
{

    private PlayerZero playerZero;

    public PlayerStandState(PlayerZero playerZero)
    {
        this.playerZero = playerZero;
        stateName = "stand";
        playerZero.rigi.velocity = new Vector2(0, 0);
        playerZero.anim.SetFloat("verticalSpeed", 0);


    }

    public override void execute()
    {
    }

    public override bool onEndState()
    {
        return true;
    }
}
