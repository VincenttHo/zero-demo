using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : Enemy
{

    private BatStateMachine batStateMachine;

    public float speed;
    private float originSpeed;

    public Transform attackMinPos;
    public Transform attackMaxPos;

    private GameObject player;

    public bool playerInRange;

    public Vector3 startPos;

    public string stateName;
    public Rigidbody2D rigi;
    public float reboundForce;

    [Header("反弹停止秒数")]
    public float stopReboundSec;
    [Header("反弹后重新运动秒数")]
    public float resetSpeedSec;

    void Start()
    {
        base.Start();
        originSpeed = speed;
        batStateMachine = new BatStateMachine(this);
        player = GameObject.FindGameObjectWithTag("Player");
        startPos = transform.position;
        rigi = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if(healthyPoint <= 0)
        {
            rigi.velocity = new Vector2(0, 0);
            speed = 0;
            originSpeed = 0;
            reboundForce = 0;
        }
        stateName = batStateMachine.currentState.stateName;
        batStateMachine.CheckChangeState();
        batStateMachine.currentState.execute();
        CheckPlayerInRange();
    }
    void CheckPlayerInRange()
    {
        if (player != null)
        {
            playerInRange =
                player.transform.position.x <= attackMaxPos.position.x
                && player.transform.position.x >= attackMinPos.position.x
                && player.transform.position.y <= attackMaxPos.position.y
                && player.transform.position.y >= attackMinPos.position.y;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);

        // 反弹
        if(other.CompareTag("Player") && other is CircleCollider2D)
        {
            speed = 0;
            Vector2 reboundDir = (transform.position - player.transform.position);
            reboundDir = new Vector2(reboundDir.x > 0 ? 1 : -1, reboundDir.y > 0 ? 1 : -1);
            rigi.velocity = reboundDir * reboundForce;
            Invoke("StopRebound", stopReboundSec);
        }

    }

    private void StopRebound()
    {
        rigi.velocity = new Vector2(0, 0);
        Invoke("ResetSpeed", resetSpeedSec);
    }

    private void ResetSpeed()
    {
        speed = originSpeed;
    }

}
