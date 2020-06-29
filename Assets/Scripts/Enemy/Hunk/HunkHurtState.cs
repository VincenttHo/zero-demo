using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HunkHurtState : BaseState
{

    private Hunk hunk;

    public HunkHurtState(Hunk hunk)
    {
        this.hunk = hunk;
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
