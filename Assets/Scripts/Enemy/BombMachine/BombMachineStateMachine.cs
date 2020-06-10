using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * 状态机
 * @author VincentHo
 * @date 2020-06-10
 */
public class BombMachineStateMachine : EnemyStateMachine
{
    
    private BombMachine bombMachine;

    public BombMachineStateMachine(BombMachine bombMachine)
    {
        this.bombMachine = bombMachine;
        currentState = new BombMachineGuardState(bombMachine);
    }

    /**
     * 状态切换逻辑
     * 所有状态：防御状态，攻击状态，死亡状态
     * 初始状态：防御状态
     * 一、防御状态：
     * 1.防御状态-->攻击状态：主角进入攻击范围内，且攻击CD结束
     * 2.防御状态-->死亡状态：hp<=0
     * 二、攻击状态：
     * 1.攻击状态-->防御状态：主角不在攻击范围内或攻击后进入CD
     * 2.攻击状态-->死亡状态：hp<=0
     */
    public void CheckChangeState()
    {
        if(bombMachine.healthyPoint <= 0)
        {
            DoChangeState(new BombMachineDeadState(bombMachine));
        }

        if(currentState is BombMachineGuardState)
        {
            if(bombMachine.attackCoolDownSec <= 0 && bombMachine.playerInRange)
            {
                DoChangeState(new BombMachineAttackState(bombMachine));
            }
        }

        if (currentState is BombMachineAttackState)
        {
            if (bombMachine.attackCoolDownSec > 0)
            {
                DoChangeState(new BombMachineGuardState(bombMachine));
            }
        }
    }

}
