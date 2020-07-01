using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSlideWallState : BaseState
{

    private PlayerZero playerZero;
    private bool canJump;

    public PlayerSlideWallState(PlayerZero playerZero)
    {
        this.playerZero = playerZero;
        stateName = "slideWall";
        playerZero.anim.SetBool("isWall", true);
        SoundManager.PlayAudioLoop(SoundManager.slidingwall);
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
        if (!PlayerController.jump)
        {
            canJump = true;
        }
        if (playerZero.wallJumpWaitTime <= 0)
        {
            
            //if (Input.GetKeyDown(KeyCode.U) && playerZero.isTouchingWall)
            if(PlayerController.jump && playerZero.isTouchingWall && canJump)
            {
                SoundManager.PlayAudio(SoundManager.walljump);
                canJump = false;
                playerZero.rigi.velocity = new Vector2(playerZero.wallJumpXSpeed * -playerZero.input, playerZero.wallJumpYSpeed);
                //playerZero.rigi.velocity = new Vector2(playerZero.rigi.velocity.x, playerZero.wallJumpYSpeed);
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
        SoundManager.StopAudioLoop(SoundManager.slidingwall);
        playerZero.anim.SetBool("isWall", false);
        return true;
    }
}
