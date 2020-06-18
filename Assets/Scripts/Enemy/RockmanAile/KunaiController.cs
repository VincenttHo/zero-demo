using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KunaiController : MonoBehaviour
{

    public float xSpeed = 10f;
    public float ySpeed = 1f;
    public float damage;
    private Vector3 lastPos;

    private Rigidbody2D rigi;
    public Transform groundCheck;
    public float checkRadius;

    private float xDir;
    private float yDir;

    private void Start()
    {
        lastPos = transform.position;
        rigi = GetComponent<Rigidbody2D>();
        xDir = transform.right.x;
        yDir = transform.up.y;

    }

    private void Update()
    {
        rigi.velocity = new Vector2(xSpeed * xDir, ySpeed * yDir);
        CheckGround();
    }

    private void FixedUpdate()
    {
        //rigi.velocity = new Vector2(xSpeed * transform.right.x, ySpeed * transform.up.y);
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

    private void OnCollisionEnter2D(Collision2D other)
    {
        //transform.rotation = Quaternion.Euler(0, transform.rotation.y == 0 ? 180 : 0, 0);
        ContactPoint2D contactPoint = other.contacts[0];//获取接触点
        // 计算入射向量
        Vector3 incidenceNm = (transform.position - lastPos).normalized;
        //计算反射向量，contactPoint.normal是碰撞面的法线
        Vector3 newDir = Vector3.Reflect(incidenceNm, contactPoint.normal).normalized;
        xDir = newDir.x > 0 ? Math.Abs(xDir) : -Math.Abs(xDir);
        yDir = newDir.y > 0 ? Math.Abs(yDir) : -Math.Abs(yDir);
        //xSpeed = newDir.x * 10;
        //ySpeed = newDir.y * 10;
        lastPos = contactPoint.point;
    }

    void CheckGround()
    {
        if(Physics2D.OverlapCircle(groundCheck.position, checkRadius, LayerMask.GetMask("Ground")))
        {
            Destroy(gameObject);
        }
    }

}
