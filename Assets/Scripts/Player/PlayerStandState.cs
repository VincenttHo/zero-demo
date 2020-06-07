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
    }

    public override void execute()
    {
    }

    public override bool onEndState()
    {
        return true;
    }
}
