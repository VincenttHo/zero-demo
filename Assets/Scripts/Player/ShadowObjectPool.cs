using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowObjectPool : MonoBehaviour
{

    private Queue<GameObject> shadowQueue;
    public GameObject shadow;
    public int maxCount;
    public float activeSec;

    private void Start()
    {
        InitShadow();
    }

    private void InitShadow()
    {
        shadowQueue = new Queue<GameObject>();
        for (int n = 0; n < maxCount; n++)
        {
            GameObject.Instantiate(shadow);
            shadow.transform.SetParent(transform);
            shadow.SetActive(false);
            shadowQueue.Enqueue(shadow);
        }
    }

    public GameObject GetShadow()
    {
        GameObject newShadow = shadowQueue.Dequeue();
        return newShadow;
    }

}
