using BehaviorDesigner.Runtime.Tasks.Unity.Timeline;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : BaseState
{

    private PlayerZero playerZero;

    public PlayerMoveState(PlayerZero playerZero)
    {
        this.playerZero = playerZero;
    }

    public override void execute()
    {
        playerZero.anim.SetFloat("HorizontalSpeed", Math.Abs(playerZero.currentHorizontalSpeed));
    }

    public override bool onEndState()
    {
        playerZero.anim.SetFloat("HorizontalSpeed", 0);
        return true;
    }
}
