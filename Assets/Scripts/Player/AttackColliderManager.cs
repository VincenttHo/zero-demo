using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackColliderManager : MonoBehaviour
{

    private PolygonCollider2D[] colliders;
    private PlayerZero zero;

    // Start is called before the first frame update
    void Start()
    {
        colliders = GetComponentsInChildren<PolygonCollider2D>();
        zero = GetComponent<PlayerZero>();
    }

    private void Update()
    {
        if(!zero.isAttack)
        {
            clearAllCollider();
        }
    }

    public void enableCollider(int index)
    {
        colliders[index].enabled = true;
    }

    public void disableCollider(int index)
    {
        colliders[index].enabled = false;
    }

    void clearAllCollider()
    {
        foreach(PolygonCollider2D collider in colliders)
        {
            collider.enabled = false;
        }
    }

    

}
