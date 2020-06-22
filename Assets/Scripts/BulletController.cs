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
    private Rigidbody2D rigi;

    private void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        BoxCollider2D cameraCollider = mainCamera.GetComponent<BoxCollider2D>();
        cameraSize = cameraCollider.bounds.size.x / 2;
        targetPos = new Vector3(transform.position.x + 100f * transform.right.x, transform.position.y, transform.position.z);
        rigi = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {

        //transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
        rigi.velocity = transform.right * speed;

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
        if (collision.gameObject.tag == "Enemy")
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            enemy.GetDamage(damage);
        }
        if (collision.gameObject.tag == "Boss")
        {
            Boss boss = collision.gameObject.GetComponent<Boss>();
            boss.GetDamage(damage, "gun");
        }
        if (collision.gameObject.tag == "Player")
        {
            if(collision.GetType().ToString() == "UnityEngine.CircleCollider2D")
            {
                PlayerZero zero = collision.gameObject.GetComponent<PlayerZero>();
                zero.GetDamage(damage);
                Destroy(gameObject);
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

}
