using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{

    public float speed = 10f;
    private GameObject mainCamera;
    private float cameraSize;
    private Vector3 targetPos;
    public float destoryDistance;
    public float damage;

    private void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        BoxCollider2D cameraCollider = mainCamera.GetComponent<BoxCollider2D>();
        cameraSize = cameraCollider.bounds.size.x / 2;
        targetPos = new Vector3(transform.position.x + 100f * transform.right.x, transform.position.y, transform.position.z);
    }

    private void Update()
    {
        
        transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);

        float cameraX = mainCamera.transform.position.x;
        if (transform.position.x > targetPos.x)
        {
            if (transform.position.x < cameraX - cameraSize - destoryDistance)
            {
                Destroy(gameObject);
            }
        }
        else if (transform.position.x < targetPos.x)
        {
            if (transform.position.x > cameraX + cameraSize + destoryDistance)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        print(collision.gameObject.tag);
        if (collision.gameObject.tag == "Player" && collision.GetType().ToString() == "UnityEngine.CircleCollider2D")
        {
            Zero zero = collision.gameObject.GetComponent<Zero>();
            zero.GetDamage(damage);
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "Enemy")
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            enemy.GetDamage(damage);
            Destroy(gameObject);
        }
    }

}
