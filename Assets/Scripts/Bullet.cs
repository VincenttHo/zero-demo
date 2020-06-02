using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public float speed = 10f;
    private float dir;
    public Renderer background;
    private float boundsX;
    private Vector3 targetPos;
    public string target;
    public int damage = 1;

    private void Start()
    {
        dir = transform.rotation.y == 0 ? -1 : 1;
        boundsX = background.bounds.size.x / 2;
        targetPos = new Vector3((boundsX + 10f) * dir, transform.position.y, transform.position.z);

    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
        
        if (Math.Abs(transform.position.x) > boundsX)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == target)
        {
            if (collision.gameObject.tag == "Player")
            {
                Zero zero = collision.gameObject.GetComponent<Zero>();
                zero.GetDamage(damage);
            }
            if (collision.gameObject.tag == "Enemy")
            {
                Enemy enemy = collision.gameObject.GetComponent<Enemy>();
                enemy.GetDamage(damage);
            }
            Destroy(gameObject);
        }
    }

}
