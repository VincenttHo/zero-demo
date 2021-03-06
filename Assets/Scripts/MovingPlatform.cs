﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{

    public Transform[] movePos;
    private int posIndex;
    public float moveSpeed;
    public float waitTime;
    private float currentWaitTime;
    private Transform playerPos;
    private Transform playerDefParent;

    void Start()
    {
        posIndex = 1;
        currentWaitTime = waitTime;
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if(player != null)
        {
            playerPos = player.transform;
            playerDefParent = playerPos.parent;
        }
    }

    void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, movePos[posIndex].position, moveSpeed * Time.deltaTime);
        // 如果距离相近,则进行倒计时向下一个位置移动
        if(Vector2.Distance(transform.position, movePos[posIndex].position) <= 0.1f)
        {
            if(currentWaitTime > 0)
            {
                currentWaitTime -= Time.deltaTime;
            } 
            else
            {
                posIndex = posIndex == 0 ? 1 : 0;
                currentWaitTime = waitTime;
            }
        }

        /*if(playerPos != null && playerPos.parent == transform && PlayerController.jump)
        {
            playerPos.parent = playerDefParent;
        }*/

       
    }

    IEnumerator ResetBoxCollider()
    {
        yield return new WaitForSeconds(0.2f);
        GetComponent<BoxCollider2D>().enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player") && other.GetType().ToString() == "UnityEngine.CapsuleCollider2D")
        {
            if(playerPos != null)
            {
                playerPos.parent = transform;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") && other.GetType().ToString() == "UnityEngine.CapsuleCollider2D")
        {
            if (PlayerController.instance.inputDown && PlayerController.instance.jump)
            {
                playerPos.parent = playerDefParent;
                GetComponent<BoxCollider2D>().enabled = false;
                StartCoroutine(ResetBoxCollider());

            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && other.GetType().ToString() == "UnityEngine.CapsuleCollider2D")
        {
            if (playerPos != null)
            {
                playerPos.parent = playerDefParent;
            }
        }
    }

}
