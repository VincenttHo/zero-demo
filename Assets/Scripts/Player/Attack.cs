using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    // 攻击伤害
    public int damage;

    private bool canHurt;

    private void Start()
    {
        canHurt = true;
    }

    // 如果攻击打到敌人，调用敌人的受伤方法给予伤害
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(canHurt)
        {
            if (collision.gameObject.tag == "Enemy")
            {
                collision.GetComponent<Enemy>().GetDamage(damage);
                canHurt = false;
            }

            if (collision.gameObject.tag == "Boss")
            {
                collision.GetComponent<Boss>().GetDamage(damage, "slash");
                canHurt = false;
            }
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        canHurt = true;
    }

}
