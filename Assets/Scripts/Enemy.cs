using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public int healthyPoint = 3;

    public int damage = 1;

    private SpriteRenderer spriteRenderer;

    public float flashTime = 0.2f;

    private Color originColor;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originColor = spriteRenderer.color;
    }

    public void GetDamage(int damage)
    {
        healthyPoint -= damage;
        FlashColor();
        if (healthyPoint <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void FlashColor()
    {
        spriteRenderer.color = Color.red;
        Invoke("ResetColor", flashTime);
    }

    private void ResetColor()
    {
        spriteRenderer.color = originColor;
    }

}
