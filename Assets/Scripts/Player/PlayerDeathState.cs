using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeathState : BaseState
{

    private PlayerZero playerZero;

    public PlayerDeathState(PlayerZero playerZero)
    {
        this.playerZero = playerZero;
        stateName = "death";
    }

    public override void execute()
    {
        
    }

    public override bool onEndState()
    {
        return true;
    }
}
