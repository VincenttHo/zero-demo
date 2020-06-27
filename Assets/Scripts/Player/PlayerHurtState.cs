using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHurtState : BaseState
{

    private PlayerZero playerZero;
    private bool canAudio;

    public PlayerHurtState(PlayerZero playerZero)
    {
        stateName = "hurt";
        canAudio = true;
    }

    public override void execute()
    {
    }

    public override bool onEndState()
    {
        return true;
    }
}
