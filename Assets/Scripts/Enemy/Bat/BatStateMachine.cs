using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * 状态机
 * @author VincentHo
 * @date 2020-06-03
 */
public class BatStateMachine : EnemyStateMachine
{
    
    private Bat bat;
    private GameObject player;

    public BatStateMachine(Bat bat)
    {
        this.bat = bat;
        player = GameObject.FindGameObjectWithTag("Player");
        currentState = new BatIdleState(bat);
    }

    /**
     * 状态切换逻辑
     * 所有状态：漫游状态，追踪状态，死亡状态
     * 初始状态：漫游状态
     */
    public void CheckChangeState()
    {
        if(bat.healthyPoint <= 0)
        {
            DoChangeState(new BatDeadState(bat));
        }

        if(currentState is BatIdleState)
        {
            if(bat.playerInRange)
            {
                DoChangeState(new BatAttackState(bat));
            }
        }

        else if (currentState is BatAttackState)
        {
            if (!bat.playerInRange)
            {
                DoChangeState(new BatIdleState(bat));
            }
        }
    }

}
