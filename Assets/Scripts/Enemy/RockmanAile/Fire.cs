

using System;
using UnityEngine;


public class Fire : MonoBehaviour
{

    public float damage;

    public void DoDestory()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (collision.GetType().ToString() == "UnityEngine.CircleCollider2D")
            {
                PlayerZero zero = collision.gameObject.GetComponent<PlayerZero>();
                zero.GetDamage(damage);
            }
        }
    }

}
