using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHurtState : BaseState
{

    private PlayerZero playerZero;

    public PlayerHurtState(PlayerZero playerZero)
    {
        stateName = "hurt";
    }

    public override void execute()
    {
    }

    public override bool onEndState()
    {
        return true;
    }
}
