using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hunk : Enemy
{

    private HunkStateMachine hunkStateMachine;

    
    [HideInInspector]
    public AnimatorStateInfo animatorStateInfo;

    public Transform leftMovePos;
    public Transform rightMovePos;
    public Transform movePos;
    public float runSpeed = 5f;
    public float runWaitTime = 1f;

    public float attackRange = 8f;
    public float attackWaitTime = 2f;
    public float attackDelay = 1f;
    //public float touchDamage = 2f;
    public GameObject bullet;
    public Transform bulletPos;
    public string stateName;


    void Start()
    {
        base.Start();
        hunkStateMachine = new HunkStateMachine(this);
    }

    void Update()
    {
        stateName = hunkStateMachine.currentState.stateName;
        hunkStateMachine.CheckChangeState();
        hunkStateMachine.currentState.execute();
    }

    public void InitBullet()
    {
        bullet.transform.position = bulletPos.position;
        bullet.transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y == 0 ? 180 : 0, transform.rotation.z);
        Instantiate(bullet);
    }

    public int GetDir()
    {
       return transform.rotation.y == 0 ? -1 : 1;
    }

    public void LookLeft()
    {
        transform.localRotation = Quaternion.Euler(new Vector3(transform.rotation.x, 0, transform.rotation.z));
    }

    public void LookRight()
    {
        transform.localRotation = Quaternion.Euler(new Vector3(transform.rotation.x, 180, transform.rotation.z));
    }

    

}
