using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackColliderManager : MonoBehaviour
{

    private PolygonCollider2D[] colliders;
    private PlayerStateManager playerStateManager;
    public GameObject bullet;
    public Transform bulletPos;

    // Start is called before the first frame update
    void Start()
    {
        colliders = GetComponentsInChildren<PolygonCollider2D>();
        playerStateManager = GetComponentInParent<PlayerStateManager>();
    }

    private void Update()
    {
        if(!playerStateManager.isAttack)
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

    void InitBullet()
    {
        bullet.transform.position = bulletPos.position;
        float rotationY = transform.parent.localScale.x == -1 ? 0 : 180;
        bullet.transform.rotation = Quaternion.Euler(0, rotationY, 0);
        Instantiate(bullet);
    }

}
