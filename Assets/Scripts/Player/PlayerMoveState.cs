using BehaviorDesigner.Runtime.Tasks.Unity.Timeline;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : BaseState
{

    private PlayerZero playerZero;
    private float playWaitSec;

    public PlayerMoveState(PlayerZero playerZero)
    {
        this.playerZero = playerZero;
        stateName = "move";
    }

    public override void execute()
    {
        if(playWaitSec <= 0)
        {
            SoundManager.PlayAudio(SoundManager.run);
            playWaitSec = playerZero.stepSec;
        }
        else
        {
            playWaitSec -= Time.deltaTime;
        }

        playerZero.rigi.velocity = new Vector2(playerZero.currentHorizontalSpeed, playerZero.rigi.velocity.y);
        playerZero.anim.SetBool("isRun", true);
    }

    public override bool onEndState()
    {
        SoundManager.StopAudioLoop(SoundManager.run);
        playerZero.rigi.velocity = new Vector2(0, playerZero.rigi.velocity.y);
        playerZero.anim.SetBool("isRun", false);
        return true;
    }
}
