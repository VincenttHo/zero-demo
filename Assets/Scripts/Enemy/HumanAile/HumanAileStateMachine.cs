using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * 状态机
 * @author VincentHo
 * @date 2020-06-03
 */
public class HumanAileStateMachine : EnemyStateMachine
{
    
    private HumanAile humanAile;
    private GameObject player;
    private PlayerZero zero;

    public HumanAileStateMachine(HumanAile humanAile)
    {
        this.humanAile = humanAile;
        currentState = new HumanAileRunState(humanAile);
        player = GameObject.FindGameObjectWithTag("Player");
        if(player != null)
        {
            zero = player.GetComponent<PlayerZero>();
        }
    }

    /**
     * 状态切换逻辑
     * 所有状态：站立状态，跑步攻击状态，跳跃攻击状态，死亡状态
     * 初始状态：站立状态
     */
    public void CheckChangeState()
    {
        if (zero == null) return;

        /*if (currentState is HumanAileIdleState)
        {
            if(zero.isGrounded && Math.Abs(zero.transform.position.x - humanAile.transform.position.x) > 0.1)
            {
                DoChangeState(new HumanAileRunState(humanAile));
            }
        }*/

        /*if (currentState is HumanAileRunState)
        {
            if (!zero.isGrounded && Math.Abs(zero.transform.position.x - humanAile.transform.position.x) <= humanAile.jumpCheckXDistance)
            {
                DoChangeState(new HumanAileIdleState(humanAile));
            }
        }*/
    }

}
