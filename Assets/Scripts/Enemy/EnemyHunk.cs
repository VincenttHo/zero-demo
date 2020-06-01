using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHunk : Enemy
{
    /** 属性参数 */
    // 敌人行走速度
    public float runSpeed = 5f;
    // 未遭遇玩家时，下一次漫游等待时间
    private float waitTime;
    public float startWaitTime = 1f; 
    // 敌人感知攻击距离
    public float attackDistance = 0.7f;
    // 是否在攻击状态
    public bool isAttack;
    // 是否在移动
    private bool isMove;
    public float distance;

    /** 组件 */
    private Animator anim;
    private AnimatorStateInfo animatorState;
    public Transform leftMovePos;
    public Transform rightMovePos;
    public Transform movePos;
    private Transform playerPos;
    public GameObject bullet;
    public Transform bulletPos;


    void Start()
    {
        base.Start();
        anim = GetComponent<Animator>();
        movePos.position = GetRandomPos();
        playerPos = GameObject.Find("Player").transform;
        animatorState = anim.GetCurrentAnimatorStateInfo(0);
    }

    void Update()
    {
        Run();
        Attack();
    }

    private void Run()
    {
        if(!isAttack)
        {
            
            if (Math.Abs(transform.position.x - movePos.position.x) > 0.1)
            {
                // 如果当前位置不在随机位置，则进行移动
                isMove = true;
                Flip();
                transform.position = Vector3.MoveTowards(transform.position, movePos.position, runSpeed * Time.deltaTime);
                anim.SetFloat("HorizontalSpeed", 1);
            }
            else
            {
                // 如果当前位置在随机位置，则开始倒计时，倒计时结束后生成新的随机位置，执行下一次移动
                isMove = false;
                anim.SetFloat("HorizontalSpeed", 0);
                if (waitTime <= 0)
                {
                    movePos.position = GetRandomPos();
                    waitTime = startWaitTime;
                }
                else
                {
                    waitTime -= Time.deltaTime;
                }
            }
        }
        
    }

    // 敌人翻转方法
    void Flip()
    {
        if (transform.position.x < movePos.position.x)
        {
            LookRight();
        }
        else
        {
            LookLeft();
        }
    }

    // 漫游随机位置生成
    Vector3 GetRandomPos()
    {
        Vector3 randomPos = new Vector3(UnityEngine.Random.Range(leftMovePos.position.x, rightMovePos.position.x), transform.position.y, transform.position.z);
        return randomPos;
    }

    void Attack()
    {
        distance = Math.Abs(transform.position.x - playerPos.position.x);
        //if(Input.GetKey(KeyCode.J))
        if(distance <= attackDistance)
        {
            anim.SetBool("IsAttack", true);
            // 如果正在移动，则停止移动
            if (isMove)
            {
                movePos.position = transform.position;
            }

            if (transform.position.x < playerPos.position.x)
            {
                LookRight();
            }
            else
            {
                LookLeft();
            }
            
            isAttack = true;
        } else
        {
            anim.SetBool("IsAttack", false);
        }
    }

    void LookLeft()
    {
        transform.localRotation = Quaternion.Euler(new Vector3(transform.rotation.x, 0, transform.rotation.z));
    }

    void LookRight()
    {
        transform.localRotation = Quaternion.Euler(new Vector3(transform.rotation.x, 180, transform.rotation.z));
    }

    public void Shoot()
    {
        bullet.transform.position = bulletPos.position;
        bullet.transform.rotation = transform.rotation;
        Instantiate(bullet);
    }

    public void endAttack()
    {
        isAttack = false;
    }

}
