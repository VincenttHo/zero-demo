using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackColliderManager : MonoBehaviour
{

    private PolygonCollider2D[] colliders;
    public GameObject bullet;
    public GameObject lv1Bullet;
    public GameObject lv2Bullet;
    public Transform bulletPos;
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

    void InitBullet()
    {
        //bullet.transform.position = bulletPos.position;
        //float rotationY = transform.localScale.x < 0 ? 0 : 180;
        //bullet.transform.rotation = Quaternion.Euler(0, rotationY, 0);
        SoundManager.PlayAudio(SoundManager.shoot);
        var initBullet = bullet;
        if(zero.gunChargeLv == 1)
        {
            initBullet = lv1Bullet;
        }
        else if(zero.gunChargeLv == 2)
        {
            initBullet = lv2Bullet;
        }
        GameObject newBullet = Instantiate(initBullet);
        newBullet.transform.position = bulletPos.position;
        newBullet.transform.rotation = zero.transform.rotation;
        zero.gunChargeLv = 0;
    }

}
