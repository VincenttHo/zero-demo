using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    private float healthMax;
    private Image healthBar;
    private Zero zero;

    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        zero = player.GetComponent<Zero>();
        healthMax = zero.hp;
        zero.hp = zero.hp - 2;
        healthBar = GetComponent<Image>();
    }

    void Update()
    {
        healthBar.fillAmount = zero.hp / healthMax;
    }
}
