using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{

    /**属性*/
    // 触碰伤害值
    public float touchDamage = 1;
    // 伤害闪烁时间
    public float damageFlashTime = 0.1f;
    // 精灵原始颜色（用于闪烁还原）
    protected Color originColor;

    /**组件*/
    protected SpriteRenderer spriteRenderer;
    public GameObject bloodEffect;
    private EnemyDropItemConfig enemyDropItemConfig;

    /**掉落物*/
    public Item[] dropItems;

    protected void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originColor = spriteRenderer.color;
        enemyDropItemConfig = GetComponent<EnemyDropItemConfig>();
    }

    // 受伤方法
    public void GetDamage(float damage)
    {

        AileHpManager.currentHp -= damage;
        FlashColor();
        // 增加流血效果（试验，并不好看）
        //bloodEffect.transform.position = new Vector3(transform.position.x, transform.position.y + 1, -5);
        //Instantiate(bloodEffect);

        // 攻击时摄影机抖动效果（试验，效果并不好）
        //CameraShakeController.cameraShake.shake();

        if (AileHpManager.currentHp <= 0)
        {
            if(enemyDropItemConfig != null)
            {
                Item item = DropItemUtil.RandomItem(enemyDropItemConfig.items);
                if (item != null)
                {
                    item.transform.position = new Vector3(transform.position.x, transform.position.y + 1, item.transform.position.z);
                    Instantiate(item);
                }
            }
            //Destroy(gameObject);
        }
    }

    // 受伤闪烁
    protected void FlashColor()
    {
        spriteRenderer.color = Color.red;
        Invoke("ResetColor", damageFlashTime);
    }

    // 重置颜色
    protected void ResetColor()
    {
        spriteRenderer.color = originColor;
    }

    protected void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && other is CircleCollider2D)
        {
            other.gameObject.GetComponent<PlayerZero>().GetDamage(touchDamage);
        }
    }

}
