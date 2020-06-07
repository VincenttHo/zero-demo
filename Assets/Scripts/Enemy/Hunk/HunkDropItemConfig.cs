using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HunkDropItemConfig : EnemyDropItemConfig
{

    public Item healthItemS;
    public Item healthItemL;

    public void Start()
    {

        healthItemS.dropRate = 0.2f;
        healthItemL.dropRate = 0.1f;
        items = new Item[] { healthItemS, healthItemL };

    }

}
