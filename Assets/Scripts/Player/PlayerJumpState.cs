using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : BaseState
{

    private PlayerZero playerZero;

    private bool canDoubleJump = true;

    public PlayerJumpState(PlayerZero playerZero)
    {
        this.playerZero = playerZero;
    }

    public override void execute()
    {
        if (playerZero.isGrounded)
        {
            playerZero.rigi.velocity = new Vector2(playerZero.rigi.velocity.x, playerZero.jumpSpeed);
            canDoubleJump = true;
        }
        else
        {
            if (canDoubleJump)
            {
                playerZero.rigi.velocity = new Vector2(playerZero.rigi.velocity.x, playerZero.jumpSpeed);
                canDoubleJump = false;
            }
        }
        playerZero.anim.SetFloat("VerticalSpeed", playerZero.rigi.velocity.y);
    }

    public override bool onEndState()
    {
        return true;
    }
}
