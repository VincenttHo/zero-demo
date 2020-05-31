using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHunk : Enemy
{
    public float runSpeed = 5f;

    private Animator anim;

    public Transform leftMovePos;
    public Transform rightMovePos;
    public Transform movePos;

    private float waitTime;
    public float startWaitTime = 1f;

    void Start()
    {
        base.Start();
        anim = GetComponent<Animator>();
        movePos.position = GetRandomPos();
    }

    void Update()
    {
        Run();
    }

    private void Run()
    {
        /*rigi.velocity = new Vector2(dir * runSpeed, rigi.velocity.y);
        anim.SetFloat("HorizontalSpeed", Math.Abs(rigi.velocity.x));*/
        if(Math.Abs(transform.position.x - movePos.position.x) > 0.1)
        {
            if(transform.position.x < movePos.position.x)
            {
                Flip(180);
            }
            else
            {
                Flip(0);
            }
            transform.position = Vector3.MoveTowards(transform.position, movePos.position, runSpeed * Time.deltaTime);
            anim.SetFloat("HorizontalSpeed", 1);
        } else
        {
            anim.SetFloat("HorizontalSpeed", 0);
            if (waitTime <= 0)
            {
                movePos.position = GetRandomPos();
                waitTime = startWaitTime;
            }else
            {
                waitTime -= Time.deltaTime;
            }
        }
        
    }

    void Flip(float rotationY)
    {
        transform.localRotation = Quaternion.Euler(new Vector3(transform.rotation.x, rotationY, transform.rotation.z));
    }

    Vector3 GetRandomPos()
    {
        Vector3 randomPos = new Vector3(UnityEngine.Random.Range(leftMovePos.position.x, rightMovePos.position.x), transform.position.y, transform.position.z);
        //Vector2 randomPos = new Vector2(leftMovePos.position.x, transform.position.y);
        return randomPos;
    }

}
