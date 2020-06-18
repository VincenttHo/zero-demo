using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonController : MonoBehaviour
{

    public float speed;
    public float destorySec;
    private Rigidbody2D rigi;
    public float damage;

    private void Start()
    {
        rigi = GetComponent<Rigidbody2D>();
        Invoke("DoDestory", destorySec);
    }

    private void Update()
    {
        rigi.velocity = new Vector2(speed * transform.right.x, 0);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && other.GetType().ToString() == "UnityEngine.CircleCollider2D")
        {
            PlayerZero zero = other.gameObject.GetComponent<PlayerZero>();
            zero.GetDamage(damage);
            Destroy(gameObject);
        }
    }

    void DoDestory()
    {
        Destroy(gameObject);
    }

}
