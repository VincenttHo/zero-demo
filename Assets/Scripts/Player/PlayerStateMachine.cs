using BehaviorDesigner.Runtime.Tasks.Unity.UnityCharacterController;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * 状态机
 * @author VincentHo
 * @date 2020-06-03
 */
public class PlayerStateMachine : MonoBehaviour
{
    
    private PlayerZero playerZero;
    public BaseState currentState;


    public PlayerStateMachine(PlayerZero playerZero)
    {
        this.playerZero = playerZero;
        currentState = new PlayerStandState(playerZero);
    }

    /**
     * 状态切换逻辑
     * 所有状态：站立状态，跑步状态，冲刺状态，跳跃状态，死亡状态
     * 初始状态：站立状态
     * 一、站立状态
     * 1. 跑步：水平速度为跑步速度
     * 2. 冲刺：水平速度为冲刺速度
     * 3. 跳跃：按下跳跃按钮
     * 4. 死亡：hp为0
     * 二、跑步状态
     * 1. 站立：水平速度为0
     * 2. 冲刺：水平速度为冲刺速度
     * 3. 跳跃：按下跳跃按钮
     * 4. 死亡：hp为0
     * 三、冲刺状态
     * 1. 站立：水平速度为0
     * 2. 跑步：水平速度为跑步速度
     * 3. 跳跃：按下跳跃按钮
     * 4. 死亡：hp为0
     * 四、跳跃状态
     * 1. 站立：触碰地面
     * 2. 死亡：hp为0
     */
    public void CheckChangeState()
    {
        if (playerZero.hp <= 0)
        {
            DoChangeState(new PlayerDeathState(playerZero));
            return;
        }

        if (playerZero.isHurt)
        {
            DoChangeState(new PlayerHurtState(playerZero));
        }

        // 一、站立
        if (currentState is PlayerStandState)
        {
            if (Math.Abs(playerZero.currentHorizontalSpeed) == playerZero.runSpeed && !playerZero.isHurt)
            {
                DoChangeState(new PlayerMoveState(playerZero));
            }
            else if(Math.Abs(playerZero.currentHorizontalSpeed) == playerZero.dashSpeed && playerZero.canDash && !playerZero.isHurt)
            {
                DoChangeState(new PlayerDashState(playerZero));
            }
            else if(playerZero.yInput > 0 && playerZero.canJump && !playerZero.isHurt && !PlayerController.instance.inputDown)
            {
                DoChangeState(new PlayerJumpState(playerZero));
            }
            else if (!playerZero.isGrounded && playerZero.rigi.velocity.y < 0)
            {   
                DoChangeState(new PlayerFallState(playerZero));
            }
            /*else if (playerZero.isTouchingPlatform() && playerZero.rigi.velocity.y < 0)
            {
                DoChangeState(new PlayerFallState(playerZero));
            }*/
        }

        // 二、跑步
        if(currentState is PlayerMoveState)
        {
            if (playerZero.currentHorizontalSpeed == 0)
            {
                DoChangeState(new PlayerStandState(playerZero));
            }
            else if (Math.Abs(playerZero.currentHorizontalSpeed) == playerZero.dashSpeed && playerZero.canDash)
            {
                DoChangeState(new PlayerDashState(playerZero));
            }
            else if (playerZero.yInput == 1 && playerZero.canJump && !PlayerController.instance.inputDown)
            {
                DoChangeState(new PlayerJumpState(playerZero));
            }
            else if (playerZero.rigi.velocity.y < 0 && !playerZero.isGrounded)
            {
                DoChangeState(new PlayerFallState(playerZero));
            }
        }
        
        // 三、冲刺
        if(currentState is PlayerDashState)
        {
            if (!playerZero.canDash)
            {
                if(playerZero.input != 0)
                {
                    DoChangeState(new PlayerMoveState(playerZero));
                } else
                {
                    DoChangeState(new PlayerStandState(playerZero));
                }
            }
            else
            {
                if (playerZero.currentHorizontalSpeed == 0)
                {
                    DoChangeState(new PlayerStandState(playerZero));
                }
                else if (Math.Abs(playerZero.currentHorizontalSpeed) == playerZero.runSpeed)
                {
                    DoChangeState(new PlayerMoveState(playerZero));
                }
                else if (playerZero.yInput == 1 && !PlayerController.instance.inputDown)
                {
                    DoChangeState(new PlayerJumpState(playerZero));
                }
            }
            
        }

        // 四、跳跃
        if(currentState is PlayerJumpState)
        {
            if (playerZero.rigi.velocity.y < 0 && !playerZero.isPlatformJump)
            {
                DoChangeState(new PlayerFallState(playerZero));
            }
            /*else if(playerZero.isTouchingWall && !playerZero.isGrounded && playerZero.yInput != 0)
            {
                DoChangeState(new PlayerJumpState(playerZero));
            }*/
            /*if(playerZero.isTouchingWall && !playerZero.isGrounded && playerZero.input != 0)
            {
                DoChangeState(new PlayerSlideWallState(playerZero));
            }*/
            /*if (playerZero.canJump && playerZero.jumpCount < playerZero.maxJumpTime)
            {
                DoChangeState(new PlayerJumpState(playerZero));
            }*/
        }

        // 五、下落
        if (currentState is PlayerFallState)
        {
            if (playerZero.isGrounded)
            {
                DoChangeState(new PlayerStandState(playerZero));
            }
            /*else if(playerZero.isTouchingWall && !playerZero.isGrounded && playerZero.yInput != 0)
            {
                DoChangeState(new PlayerJumpState(playerZero));
            }*/
            else if (playerZero.isTouchingWall && !playerZero.isGrounded && playerZero.input != 0 && GameController.instance.canControll)
            {
                DoChangeState(new PlayerSlideWallState(playerZero));
            }
            /*if (playerZero.canJump && playerZero.jumpCount < playerZero.maxJumpTime)
            {
                DoChangeState(new PlayerJumpState(playerZero));
            }*/
        }

        // 六、滑墙
        if (currentState is PlayerSlideWallState)
        {
            if (playerZero.isGrounded)
            {
                DoChangeState(new PlayerStandState(playerZero));
            }
            else if (playerZero.yInput == 1 && !playerZero.isTouchingWall && !playerZero.isGrounded)
            {
                DoChangeState(new PlayerJumpState(playerZero));
            }
            /*else if (!(playerZero.isTouchingWall && !playerZero.isGrounded && playerZero.input != 0))
            {
                DoChangeState(new PlayerFallState(playerZero));
            }*/
            else if(!GameController.instance.canControll || playerZero.input == 0 || playerZero.input != 0 && !playerZero.isTouchingWall)
            {
                DoChangeState(new PlayerFallState(playerZero));
            }
        }

        // 七、受伤
        if (currentState is PlayerHurtState)
        {
            if (playerZero.isGrounded && !playerZero.isHurt && playerZero.input == 0)
            {
                DoChangeState(new PlayerStandState(playerZero));
            }
            else if (!playerZero.isGrounded && !playerZero.isHurt)
            {
                DoChangeState(new PlayerFallState(playerZero));
            }
            else if (playerZero.isGrounded && !playerZero.isHurt && playerZero.input != 0)
            {
                DoChangeState(new PlayerFallState(playerZero));
            }
        }

    }

    public void DoChangeState(BaseState newState)
    {
        bool canChange = currentState.onEndState();
        BaseState lastState = currentState;
        if (canChange)
        {
            currentState = newState;
            currentState.lastState = lastState;
        }
    }

}
