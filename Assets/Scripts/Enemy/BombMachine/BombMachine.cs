using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombMachine : Enemy
{

    private BombMachineStateMachine bombMachineStateMachine;

    public Transform attackMinPos;
    public Transform attackMaxPos;
    public Transform bombInitPos;
    public GameObject bomb;

    public bool playerInRange;

    private GameObject player;
    public Transform playerPos;

    public float attackCoolDown;
    public float attackCoolDownSec;

    public string stateName;

    [HideInInspector]
    public Animator anim;
    [HideInInspector]
    public BoxCollider2D boxCollider;

    void Start()
    {
        base.Start();
        bombMachineStateMachine = new BombMachineStateMachine(this);
        player = GameObject.FindGameObjectWithTag("Player");
        if(player != null)
        {
            playerPos = player.transform;
        }
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            if(player != null)
            {
                playerPos = player.transform;
            }
        }
        stateName = bombMachineStateMachine.currentState.stateName;
        
        bombMachineStateMachine.CheckChangeState();
        CheckPlayerInRange();
    }

    private void LateUpdate()
    {
        bombMachineStateMachine.currentState.execute();
    }

    void CheckPlayerInRange()
    {
        if(playerPos != null)
        {
            playerInRange =
                playerPos.position.x <= attackMaxPos.position.x
                && playerPos.position.x >= attackMinPos.position.x
                && playerPos.position.y <= attackMaxPos.position.y
                && playerPos.position.y >= attackMinPos.position.y;
        }
    }

    void InitBomb()
    {
        var newBomb = GameObject.Instantiate(bomb);
        newBomb.transform.position = bombInitPos.position;
        newBomb.transform.rotation = bombInitPos.rotation;
    }

    void EndAttack()
    {
        attackCoolDownSec = attackCoolDown;
    }
}
