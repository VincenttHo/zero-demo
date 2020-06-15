using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombMachineAttackState : BaseState
{

    private BombMachine bombMachine;

    public BombMachineAttackState(BombMachine bombMachine)
    {
        if (bombMachine.playerPos != null)
        {
            bombMachine.transform.rotation = Quaternion.Euler(0, bombMachine.playerPos.position.x > bombMachine.transform.position.x ? 180 : 0, 0);
            stateName = "attack";
            this.bombMachine = bombMachine;
            bombMachine.anim.SetTrigger("attack");
        }
    }

    public override void execute()
    {
        if(bombMachine != null)
        {
            bombMachine.boxCollider.enabled = true;
        }
    }

    public override bool onEndState()
    {
        return true;
    }

}
