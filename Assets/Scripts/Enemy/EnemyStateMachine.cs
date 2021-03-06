﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * 状态机
 * @author VincentHo
 * @date 2020-06-03
 */
public class EnemyStateMachine : MonoBehaviour
{

    public BaseState currentState;

    public void DoChangeState(BaseState newState)
    {
        bool canChange = currentState.onEndState();
        if(canChange)
        {
            currentState = newState;
        }
    }

}
