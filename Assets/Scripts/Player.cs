using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * 所有角色父类
 * @author:Vincent
 * @date:2020-06-02
 */
public class Player : MonoBehaviour
{
    // 生命值
    //[HideInInspector]
    public float hp;
    public float maxHp;
    // 动画组件
    [HideInInspector]
    public Animator anim;
    protected AnimatorStateInfo animatorState;
    // 精灵组件
    private Renderer renderer;
    private CircleCollider2D circleCollider;
    [HideInInspector]
    public Rigidbody2D rigi;

    public float hurtBlinkSeconds = 1f;

    public float blinkIntervalSeconds = 0.2f;
    private bool isBlink;
    public float hurtBackDistance = 1;
    public float destoryTime;
    protected bool canHurt = true;

    protected void Start()
    {
        hp = maxHp;
        anim = GetComponent<Animator>();
        animatorState = anim.GetCurrentAnimatorStateInfo(0);
        renderer = GetComponent<Renderer>();
        circleCollider = GetComponent<CircleCollider2D>();
        rigi = GetComponent<Rigidbody2D>();
    }

    public void GetDamage(float damage)
    {
        if(canHurt)
        {
            hp -= damage;
            if (hp <= 0)
            {
                Dead();
            }
            else
            {
                circleCollider.enabled = false;
                //canHurt = false;
                isBlink = true;
                StartCoroutine(BlinkPlayer());
                StartCoroutine(NoHurt());

            }
        }
        
    }

    public void Dead()
    {
        //canHurt = false;
        circleCollider.enabled = false;
        anim.SetTrigger("dead");
        Invoke("DestoryPlayer", destoryTime);
        
    }

    void DestoryPlayer()
    {
        Destroy(gameObject);
    }

    IEnumerator BlinkPlayer()
    {
        while(isBlink)
        {
            renderer.enabled = !renderer.enabled;
            yield return new WaitForSeconds(blinkIntervalSeconds);
        }
        renderer.enabled = true;
        //canHurt = true;
        circleCollider.enabled = true;
    }

    IEnumerator NoHurt()
    {
        yield return new WaitForSeconds(hurtBlinkSeconds);
        isBlink = false;
        //canHurt = true;
        circleCollider.enabled = true;
    }

    public void HurtBack()
    {
        if(rigi != null)
        {
            rigi.velocity = new Vector2(-transform.right.x * hurtBackDistance, rigi.velocity.y);
        }
    }

}
