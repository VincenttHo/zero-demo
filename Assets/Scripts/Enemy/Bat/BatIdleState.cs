using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatIdleState : BaseState
{

    private Bat bat;

    public BatIdleState(Bat bat)
    {
        this.bat = bat;
        stateName = "idle";
    }

    public override void execute()
    {
        if(Vector3.Distance(bat.transform.position, bat.startPos) > 0.1f)
        {
            bat.transform.position = Vector3.MoveTowards(bat.transform.position, bat.startPos, bat.speed * Time.deltaTime);
        }
    }

    public override bool onEndState()
    {
        return true;
    }

}
