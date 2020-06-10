using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSlideWallState : BaseState
{

    private PlayerZero playerZero;

    public PlayerSlideWallState(PlayerZero playerZero)
    {
        this.playerZero = playerZero;
        stateName = "slideWall";
        playerZero.anim.SetBool("isWall", true);
        //playerZero.rigi.velocity = new Vector2(playerZero.rigi.velocity.x, 0);
    }

    public override void execute()
    {
        if(!WallJump())
        {
            playerZero.rigi.velocity = new Vector2(playerZero.rigi.velocity.x, Mathf.Clamp(playerZero.rigi.velocity.y, -playerZero.wallSlidingSpeed, float.MaxValue));
        }
        playerZero.anim.SetFloat("verticalSpeed", playerZero.rigi.velocity.y);
    }

    bool WallJump()
    {
        if(playerZero.wallJumpWaitTime <= 0)
        {
            if (Input.GetKeyDown(KeyCode.U) && playerZero.isTouchingWall)
            {
                playerZero.rigi.velocity = new Vector2(playerZero.wallJumpXSpeed * -playerZero.input, playerZero.wallJumpYSpeed);
                playerZero.wallJumpWaitTime = playerZero.wallJumpCD;
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            playerZero.wallJumpWaitTime -= Time.deltaTime;
            return false;
        }
        
    }

    public override bool onEndState()
    {
        playerZero.anim.SetBool("isWall", false);
        return true;
    }
}
