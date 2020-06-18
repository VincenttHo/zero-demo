

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
        
    }

}
