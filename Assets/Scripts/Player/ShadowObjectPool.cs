using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowObjectPool : MonoBehaviour
{

    public static ShadowObjectPool instance;

    private Queue<GameObject> shadowQueue;
    public GameObject shadow;
    public int maxCount;

    private void Start()
    {
        instance = this;
        InitShadow();
    }

    private void InitShadow()
    {
        shadowQueue = new Queue<GameObject>();
        for (int n = 0; n < maxCount; n++)
        {
            var newShadow = GameObject.Instantiate(shadow);
            newShadow.transform.SetParent(transform);
            EnPool(newShadow);
        }
    }

    public void EnPool(GameObject shadow)
    {
        shadow.SetActive(false);
        shadowQueue.Enqueue(shadow);
    }

    public GameObject GetShadow()
    {

        if(shadowQueue.Count == 0)
        {
            InitShadow();
        }

        var newShadow = shadowQueue.Dequeue();
        newShadow.SetActive(true);
        return newShadow;
    }

}
