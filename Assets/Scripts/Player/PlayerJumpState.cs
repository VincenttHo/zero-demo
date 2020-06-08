using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : BaseState
{

    private PlayerZero playerZero;
    public float speed;

    public PlayerJumpState(PlayerZero playerZero)
    {
        this.playerZero = playerZero;
        stateName = "jump";
    }

    public override void execute()
    {
        float horizontalSpeed = GetHorizontalSpeed();

        if(playerZero.canJump)
        {
            playerZero.rigi.velocity = new Vector2(horizontalSpeed, playerZero.jumpSpeed * playerZero.yInput);
        }
        /*else
        {
            if (canDoubleJump)
            {
                playerZero.rigi.velocity = new Vector2(horizontalSpeed, playerZero.jumpSpeed);
                canDoubleJump = false;
            }
        }*/
        playerZero.anim.SetFloat("verticalSpeed", playerZero.rigi.velocity.y);
    }

    public override bool onEndState()
    {
        return true;
    }

    private float GetHorizontalSpeed()
    {
        if(lastState is PlayerDashState)
        {
            speed = playerZero.dashSpeed;
            return playerZero.input * playerZero.dashSpeed;
        } 
        else
        {
            speed = playerZero.runSpeed;
            return playerZero.input * playerZero.runSpeed;
        }
    }
}
