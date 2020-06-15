using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanAileIdleState : BaseState
{

    private HumanAile humanAile;

    public HumanAileIdleState(HumanAile humanAile)
    {
        this.humanAile = humanAile;
    }

    public override void execute()
    {
        humanAile.anim.SetBool("isRunning", false);
    }

    public override bool onEndState()
    {
        return true;
    }

}
