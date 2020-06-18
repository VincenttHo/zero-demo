

using System;
using UnityEngine;


public class Cyclone : MonoBehaviour
{

    public float damage;
    public float distance;
    public float speed;
    public float attractForce;
    private Vector2 attackPos;

    private bool canAttract;
    private GameObject player;
    private Rigidbody2D playerRigi;
    private Rigidbody2D rigi;


    private void Start()
    {
        attackPos = new Vector2(transform.position.x + distance * transform.right.x, transform.position.y);
        player = GameObject.FindGameObjectWithTag("Player");
        rigi = GetComponent<Rigidbody2D>();
        if (player != null)
        {
            playerRigi = player.GetComponent<Rigidbody2D>();
        }
    }

    private void Update()
    {
        DoAttract();
        if (Vector2.Distance(transform.position, attackPos) > 0.5f)
        {
            rigi.velocity = transform.right * speed;
        } else
        {
            rigi.velocity = Vector2.zero;
        }
    }

    public void DoDestory()
    {
        Destroy(gameObject);
    }

    private void DoAttract()
    {
        if (player == null) return;
        if(canAttract)
        {
            float dir = transform.position.x > player.transform.position.x ? 1 : -1;
            playerRigi.velocity = new Vector2(playerRigi.velocity.x + dir * attractForce, playerRigi.velocity.y);
        }
    }

    public void EnableAttract()
    {
        canAttract = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }

}
