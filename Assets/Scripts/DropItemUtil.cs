using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItemUtil
{

    /**
     * 根据概率获取物品
     */
    public static Item RandomItem(Item[] items)
    {

        if(items != null && items.Length > 0)
        {
            Item currentItem = null;
            foreach(Item item in items)
            {
                float dropRate = item.dropRate * 100;
                float targetNum = Random.Range(1, 100);
                if(dropRate >= targetNum)
                {
                    if(currentItem == null)
                    {
                        currentItem = item;
                    }
                    else if(dropRate / 100 < currentItem.dropRate)
                    {
                        currentItem = item;
                    }
                }
            }
            return currentItem;
        } 
        else
        {
            return null;
        }
    }

}
