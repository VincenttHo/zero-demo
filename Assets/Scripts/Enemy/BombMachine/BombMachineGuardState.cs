using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombMachineGuardState : BaseState
{

    private BombMachine bombMachine;

    public BombMachineGuardState(BombMachine bombMachine)
    {
        stateName = "guard";
        this.bombMachine = bombMachine;
    }

    public override void execute()
    {
        bombMachine.boxCollider.enabled = false;
        if (bombMachine.attackCoolDownSec > 0)
        {
            bombMachine.attackCoolDownSec -= Time.deltaTime;
        }
    }

    public override bool onEndState()
    {
        return true;
    }
}
