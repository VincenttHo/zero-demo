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

    public float gunNoHurtSec;
    private bool canGunHurt;

    /**掉落物*/
    public Item[] dropItems;

    public bool canMove;

    public Animator anim;

    protected void Start()
    {
        anim = GetComponent<Animator>();
        canGunHurt = true;
        spriteRenderer = GetComponent<SpriteRenderer>();
        originColor = spriteRenderer.color;
        enemyDropItemConfig = GetComponent<EnemyDropItemConfig>();
    }

    // 受伤方法
    public void GetDamage(float damage, string type)
    {
        FlashColor();
        if(type == "gun")
        {
            if (!canGunHurt)
            {
                return;
            } 
            else
            {
                canGunHurt = false;
                StartCoroutine(NoHurtCountDown());
            }
        }
        AileHpManager.currentHp -= damage;

        if (AileHpManager.currentHp <= 0)
        {
            if (enemyDropItemConfig != null)
            {
                Item item = DropItemUtil.RandomItem(enemyDropItemConfig.items);
                if (item != null)
                {
                    item.transform.position = new Vector3(transform.position.x, transform.position.y + 1, item.transform.position.z);
                    Instantiate(item);
                }
            }
        }
        
    }

    IEnumerator NoHurtCountDown()
    {
        yield return new WaitForSeconds(gunNoHurtSec);
        canGunHurt = true;

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
        if (other.CompareTag("Player") && other is CircleCollider2D && AileHpManager.currentHp > 0)
        {
            other.gameObject.GetComponent<PlayerZero>().GetDamage(touchDamage);
        }
    }

}
