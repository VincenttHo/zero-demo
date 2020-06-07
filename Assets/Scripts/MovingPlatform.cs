using System.Collections;
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
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
        playerDefParent = playerPos.parent;
    }

    void Update()
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
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player") && other.GetType().ToString() == "UnityEngine.BoxCollider2D")
        {
            playerPos.parent = transform;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && other.GetType().ToString() == "UnityEngine.BoxCollider2D")
        {
            playerPos.parent = playerDefParent;
        }
    }

}
