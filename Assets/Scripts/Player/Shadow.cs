using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shadow : MonoBehaviour
{

    private Transform player;
    private SpriteRenderer mySpriteRenderer;
    private SpriteRenderer playerSpriteRenderer;

    [Header("显示时间参数")]
    // 残影显示开始时间
    public float activeStart;
    // 残影总共可显示时间
    public float activeTime;

    private void OnEnable()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerSpriteRenderer = player.GetComponent<SpriteRenderer>(); 
        mySpriteRenderer = GetComponent<SpriteRenderer>();

        mySpriteRenderer.sprite = playerSpriteRenderer.sprite;
        transform.position = player.position;
        transform.rotation = player.rotation;
        activeStart = Time.time;
    }

    private void Update()
    {
        if(Time.time >= activeStart + activeTime)
        {
            ShadowObjectPool.instance.EnPool(this.gameObject);
        }
    }

}
