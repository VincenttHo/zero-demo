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
    public float hp;
    // 动画组件
    protected Animator anim;
    // 精灵组件
    private Renderer renderer;
    private CapsuleCollider2D capsuleCollider;
    protected Rigidbody2D rigi;

    public float hurtBlinkSeconds = 1f;

    public float blinkIntervalSeconds = 0.2f;
    private bool isBlink;
    public float hurtBackDistance = 1;
    public float destoryTime;

    protected void Start()
    {
        anim = GetComponent<Animator>();
        renderer = GetComponent<Renderer>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        rigi = GetComponent<Rigidbody2D>();
    }

    public void GetDamage(float damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            Dead();
        } else
        {
            capsuleCollider.enabled = false;
            isBlink = true;
            StartCoroutine(BlinkPlayer());
            StartCoroutine(NoHurt());

        }
        
    }

    public void Dead()
    {
        capsuleCollider.enabled = false;
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
        capsuleCollider.enabled = true;
    }

    IEnumerator NoHurt()
    {
        yield return new WaitForSeconds(hurtBlinkSeconds);
        isBlink = false;
    }

    public void HurtBack()
    {
        rigi.velocity = new Vector2(-this.transform.localScale.x / Math.Abs(this.transform.localScale.x) * hurtBackDistance, transform.position.y);
    }

}
