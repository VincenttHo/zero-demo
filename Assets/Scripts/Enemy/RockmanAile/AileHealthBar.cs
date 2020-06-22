using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AileHealthBar : MonoBehaviour
{

    private Image healthBar;

    void Start()
    {
        healthBar = GetComponent<Image>();
    }

    void Update()
    {
        healthBar.fillAmount = AileHpManager.currentHp / AileHpManager.maxHp;
    }
}
