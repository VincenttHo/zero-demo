using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateManager : MonoBehaviour
{
    // 是否站立状态
    public bool isStand = true;
    // 是否站立状态
    public bool isRun = false;
    // 是否冲刺状态
    public bool isDash = false;
    // 是否站立状态
    public bool isJump = false;
    // 是否站立状态
    public bool isAttack = false;
    // 是否受伤状态
    public bool isHurt = false;

    public void Run()
    {
        isRun = true;
        isStand = false;
        isDash = false;
        isJump = false;
        isAttack = false;
    }

    public void Stand()
    {
        isStand = true;
        isRun = false;
        isDash = false;
        isJump = false;
        isAttack = false;
    }

    public void Jump()
    {
        isJump = true;
        isRun = false;
        isDash = false;
        isStand = false;
        isAttack = false;
    }

    public void Dash()
    {
        isDash = true;
        isRun = false;
        isJump = false;
        isStand = false;
        isAttack = false;
    }

    public void Attack()
    {
        isAttack = true;
        isRun = false;
        isJump = false;
        isStand = false;
        isDash = false;
    }

}
