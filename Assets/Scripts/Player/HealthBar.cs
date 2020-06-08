using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    private float healthMax;
    private Image healthBar;
    private Player zero;

    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        zero = player.GetComponent<Player>();
        if(zero != null)
        {
            healthMax = zero.hp;
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
