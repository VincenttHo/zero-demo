using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * 状态机
 * @author VincentHo
 * @date 2020-06-03
 */
public class RockmanAileStateMachine : EnemyStateMachine
{
    
    private HumanAile humanAile;

    public RockmanAileStateMachine(HumanAile humanAile)
    {
    }

    /**
     * 状态切换逻辑
     * 所有状态：站立状态，跑步攻击状态，跳跃攻击状态，死亡状态
     * 初始状态：站立状态
     */
    public void CheckChangeState()
    {
    }

}
