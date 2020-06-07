using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : BaseState
{

    private PlayerZero playerZero;

    public PlayerDashState(PlayerZero playerZero)
    {
        this.playerZero = playerZero;
    }

    public override void execute()
    {
        throw new NotImplementedException();
    }

    public override bool onEndState()
    {
        throw new NotImplementedException();
    }
}
