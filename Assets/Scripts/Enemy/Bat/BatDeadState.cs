using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatDeadState : BaseState
{

    private Bat bat;

    public BatDeadState(Bat bat)
    {
        this.bat = bat;
        stateName = "dead";
    }

    public override void execute()
    {
    }

    public override bool onEndState()
    {
        return true;
    }
}
