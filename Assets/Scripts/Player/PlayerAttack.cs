using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    // 属性
    public int damage = 1;
    public float endAttackSecond = 0.08f;
    public float startAttackSecond = 0.08f;
    public int HitCount = 0;

    // 组件
    private Animator anim;
    private PolygonCollider2D[] attackPolygonColliders;
    private AnimatorStateInfo animatorStateInfo;
    private Rigidbody2D rigi;
    private PlayerZero zero;

    // 动画状态
    private const string StandState = "zero_stand";
    private const string SwordAttack1State = "zero_sword_attack1";
    private const string SwordAttack2State = "zero_sword_attack2";
    private const string JumpSwordAttackState = "zero_jump_sword_attack";
    private const string RunState = "zero_run";

    void Start()
    {
        //collider = GetComponent<PolygonCollider2D>();
        anim = GetComponentInChildren<Animator>();
        attackPolygonColliders = GetComponentsInChildren<PolygonCollider2D>();
        rigi = GetComponent<Rigidbody2D>();
        zero = GetComponent<PlayerZero>();
    }

    void Update()
    {
        animatorStateInfo = anim.GetCurrentAnimatorStateInfo(0);
        if(animatorStateInfo.normalizedTime >= 0.9f && !animatorStateInfo.IsName(StandState))
        {
            HitCount = 0;
        }
        if (Input.GetKeyDown(KeyCode.Y))
        {
            Attack();
        }
        // 处理跳砍动作，如果落地，则将攻击动作复原
        if (animatorStateInfo.IsName(JumpSwordAttackState) && rigi.velocity.y == 0)
        {
            HitCount = 0;
        }
        anim.SetInteger("attack", HitCount);
        if(HitCount == 0)
        {
            zero.isAttack = false;
        }
    }

    void Attack()
    {

        if ((animatorStateInfo.IsName(StandState) || animatorStateInfo.IsName(RunState) || rigi.velocity.y != 0) && HitCount == 0)
        {
            HitCount = 1;
        }
        if (animatorStateInfo.IsName(SwordAttack1State) && HitCount == 1 && animatorStateInfo.normalizedTime < 0.8f)
        {
            HitCount = 2;
        }
        if (animatorStateInfo.IsName(SwordAttack2State) && HitCount == 2 && animatorStateInfo.normalizedTime < 0.8f)
        {
            HitCount = 3;
        }

        if(HitCount > 0 && !(zero.stateMachine.currentState is PlayerJumpState))
        {
            zero.isAttack = true;
            zero.input = 0;
            /*if (HitCount == 3)
            {
                StartCoroutine(startAttack(2));
            }
            else
            {
                attackPolygonColliders[HitCount - 1].enabled = true;
                StartCoroutine(endAttack(HitCount - 1));
            }*/
        }
        

    }

    /*IEnumerator endAttack(int index)
    {
        yield return new WaitForSeconds(endAttackSecond);
        attackPolygonColliders[index].enabled = false;
    }

    IEnumerator startAttack(int index)
    {
        yield return new WaitForSeconds(startAttackSecond);
        if(HitCount > 0)
        {
            attackPolygonColliders[HitCount - 1].enabled = true;
            StartCoroutine(endAttack(index));
        }
    }*/

}
