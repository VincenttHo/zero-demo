using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public int healthyPoint = 3;

    public int damage = 1;

    void Start()
    {
        
    }

    void Update()
    {
    }

    public void GetDamage(int damage)
    {
        healthyPoint -= damage;
        if(healthyPoint <= 0)
        {
            Destroy(gameObject);
        }
    }
}
