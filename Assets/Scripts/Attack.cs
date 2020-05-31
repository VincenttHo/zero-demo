using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    // 攻击伤害
    public int damage;

    // 如果攻击打到敌人，调用敌人的受伤方法给予伤害
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            collision.GetComponent<Enemy>().GetDamage(damage);
        }
    }

}
