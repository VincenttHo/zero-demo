using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    private float healthMax;
    private Image healthBar;
    private PlayerZero zero;

    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if(player != null)
        {
            zero = player.GetComponent<PlayerZero>();
            if (zero != null)
            {
                healthMax = zero.maxHp;
            }
        }
        healthBar = GetComponent<Image>();
    }

    void Update()
    {
        if(zero != null)
        {
            healthBar.fillAmount = zero.hp / healthMax;
        }
    }
}
