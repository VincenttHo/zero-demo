using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : Enemy
{

    private BatStateMachine batStateMachine;

    public float speed;

    public Transform attackMinPos;
    public Transform attackMaxPos;

    private GameObject player;

    public bool playerInRange;

    public Vector3 startPos;

    public string stateName;

    void Start()
    {
        base.Start();
        batStateMachine = new BatStateMachine(this);
        player = GameObject.FindGameObjectWithTag("Player");
        startPos = transform.position;
    }

    void Update()
    {
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

}
