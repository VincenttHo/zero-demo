using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AileHealthBar : MonoBehaviour
{

    public Image[] healthBars;
    private float healthBarMaxHp;
    private int healthBarIndex;
    private float currentHealthBarHp;

    void Start()
    {
        //healthBars = GetComponentsInChildren<Image>();
        healthBarMaxHp = AileHpManager.maxHp / healthBars.Length;
        healthBarIndex = 0;
        currentHealthBarHp = healthBarMaxHp;
    }

    void Update()
    {
        if(AileHpManager.currentHp >= 0)
        {
            currentHealthBarHp = AileHpManager.currentHp - (healthBars.Length - (healthBarIndex + 1)) * AileHpManager.maxHp / healthBars.Length;
            if (currentHealthBarHp < 0)
            {
                healthBars[healthBarIndex].fillAmount = 0;
                healthBarIndex++;
            }

            healthBars[healthBarIndex].fillAmount = currentHealthBarHp / healthBarMaxHp;
            //if(AileHpManager.currentHp / AileHpManager.maxHp)
        }
        else
        {
            healthBars[healthBarIndex].fillAmount = 0;
        }
    }
}
