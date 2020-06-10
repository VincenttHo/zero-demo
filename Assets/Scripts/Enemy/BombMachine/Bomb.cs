using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{

    public float xSpeed;
    public float ySpeed;
    private Rigidbody2D rigi;
    private CircleCollider2D collider;
    public float damage;
    public float destroySec;

    void Start()
    {
        collider = GetComponent<CircleCollider2D>();
        rigi = GetComponent<Rigidbody2D>();
        rigi.velocity = new Vector2(transform.right.x * xSpeed, transform.up.y * ySpeed);
    }

    void Update()
    {
        DoMove();
    }

    private void DoMove()
    {
        if (collider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            rigi.velocity = transform.right * xSpeed;
            Invoke("DestoryBomb", destroySec);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player") && other.GetType().ToString() == "UnityEngine.CircleCollider2D")
        {
            other.gameObject.GetComponent<PlayerZero>().GetDamage(damage);
            Destroy(gameObject);
        }   
    }

    private void DestoryBomb()
    {
        Destroy(gameObject);
    }

}
