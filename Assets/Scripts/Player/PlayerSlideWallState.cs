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
        playerZero.rigi.velocity = new Vector2(playerZero.rigi.velocity.x, 0);
    }

    public override void execute()
    {
        playerZero.rigi.velocity = new Vector2(playerZero.rigi.velocity.x, Mathf.Clamp(playerZero.rigi.velocity.y, -playerZero.wallSlidingSpeed, float.MaxValue));
    }

    public override bool onEndState()
    {
        playerZero.anim.SetBool("isWall", false);
        return true;
    }
}
