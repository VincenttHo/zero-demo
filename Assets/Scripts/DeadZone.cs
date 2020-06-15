using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            GameObject zero = other.gameObject;
            if(zero != null)
            {
                PlayerZero playerZero = zero.GetComponent<PlayerZero>();
                playerZero.GetDamage(1000);
            }
        }
    }

}
