﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HunkDeadState : BaseState
{

    private Hunk hunk;

    public HunkDeadState(Hunk hunk)
    {
        this.hunk = hunk;
    }

    public override void execute()
    {
    }

    public override bool onEndState()
    {
        return true;
    }
}
