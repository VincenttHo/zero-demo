using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * 状态机
 * @author VincentHo
 * @date 2020-06-03
 */
public class HunkStateMachine : EnemyStateMachine
{
    
    private Hunk hunk;
    private GameObject player;

    public HunkStateMachine(Hunk hunk)
    {
        this.hunk = hunk;
        currentState = new HunkMoveState(hunk);
        player = GameObject.FindGameObjectWithTag("Player");
    }

    /**
     * 状态切换逻辑
     * 所有状态：漫游状态，攻击状态，死亡状态
     * 初始状态：漫游状态
     * 一、漫游状态：
     * 1..漫游状态-->攻击状态：主角进入攻击范围内
     * 2.漫游状态-->死亡状态：hp<=0
     * 二、攻击状态：
     * 1.攻击状态-->漫游状态：主角离开攻击范围
     * 2.攻击状态-->死亡状态：hp<=0
     */
    public void CheckChangeState()
    {

        if (hunk.healthyPoint <= 0)
        {
            DoChangeState(new HunkDeadState(hunk));
        } 
        else if (hunk.isHurt)
        {
            DoChangeState(new HunkHurtState(hunk));
        }

        // 漫游状态
        if (currentState is HunkMoveState)
        {
            if(PlayerInAttackRange())
            {
                DoChangeState(new HunkAttackState(hunk));
            }
        }
        // 攻击状态
        else if(currentState is HunkAttackState)
        {
            if (!PlayerInAttackRange())
            {
                DoChangeState(new HunkMoveState(hunk));
            }
        }

        else if (currentState is HunkHurtState)
        {
            if (!hunk.isHurt)
            {
                DoChangeState(new HunkMoveState(hunk));
            }
        }
    }

    private bool PlayerInAttackRange()
    {
        if(player != null)
        {
            if(Math.Abs(hunk.transform.position.x - player.transform.position.x) <= hunk.attackRange)
            {
                return true;
            }
        } 
        else
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }

        return false;
    }

}
