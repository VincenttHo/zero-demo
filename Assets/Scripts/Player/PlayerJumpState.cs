using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : BaseState
{

    private PlayerZero playerZero;
    public float speed;
    private bool isWallJump;

    public PlayerJumpState(PlayerZero playerZero)
    {
        this.playerZero = playerZero;
        stateName = "jump";
        
    }

    public override void execute()
    {
        

        float horizontalSpeed = GetHorizontalSpeed();

        /*if (lastState is PlayerSlideWallState && playerZero.isTouchingWall)
        {
            playerZero.anim.SetBool("isWallJump", true);
            isWallJump = true;
        }*/

        if (playerZero.canJump)
        {
            /*if(isWallJump)
            {
                playerZero.rigi.velocity = new Vector2(-playerZero.wallJumpXSpeed, playerZero.wallJumpYSpeed * playerZero.yInput);
            }
            else
            {*/
                playerZero.rigi.velocity = new Vector2(horizontalSpeed, playerZero.jumpSpeed * playerZero.yInput);
            //}
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
        playerZero.anim.SetBool("isWallJump", false);
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
