using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HunkDeadState : EnemyBaseState
{

    private Hunk hunk;

    public HunkDeadState(Hunk hunk)
    {
        this.hunk = hunk;
    }

    public override void execute()
    {
        //hunk.Dead();
    }

    public override bool onEndState()
    {
        return true;
    }
}
