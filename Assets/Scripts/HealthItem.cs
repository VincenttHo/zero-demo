using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthItem : Item
{

    public float healthHp;
    private PlayerZero zero;

    void Start()
    {
        zero = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerZero>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            zero.hp += healthHp;
            if(zero.hp > zero.maxHp)
            {
                zero.hp = zero.maxHp;
            }
            Destroy(gameObject);
        }

    }

}
