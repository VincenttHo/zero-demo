using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFallState : BaseState
{

    private PlayerZero playerZero;
    public float horizontalSpeed;

    public PlayerFallState(PlayerZero playerZero)
    {
        if(lastState != null && lastState is PlayerJumpState)
        {
            PlayerJumpState jumpSate = (PlayerJumpState)lastState;
            horizontalSpeed = jumpSate.speed;
        }
        else 
        {
            horizontalSpeed = playerZero.runSpeed;
        }
        this.playerZero = playerZero;
        stateName = "fall";
    }

    public override void execute()
    {
        playerZero.rigi.velocity = new Vector2(horizontalSpeed * playerZero.input, playerZero.rigi.velocity.y);
        playerZero.anim.SetFloat("verticalSpeed", playerZero.rigi.velocity.y);
    }

    public override bool onEndState()
    {
        playerZero.anim.SetFloat("verticalSpeed", 0);
        return true;
    }
}
