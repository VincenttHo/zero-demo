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
    public float damage = 1f;

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
            print("打中"+ target + "了！");
            Destroy(gameObject);
        }
    }

}
