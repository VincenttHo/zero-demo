using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    public int damage = 1;

    private PolygonCollider2D collider;

    private Animator anim;

    public float endAttackSecond = 0.15f;

    void Start()
    {
        collider = GetComponent<PolygonCollider2D>();
        anim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Y))
        {
            anim.SetTrigger("SwordAttack");
            collider.enabled = true;
            StartCoroutine(endAttack());
        }
    }

    IEnumerator endAttack()
    {
        yield return new WaitForSeconds(endAttackSecond);
        collider.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Enemy")
        {
            other.GetComponent<Enemy>().GetDamage(damage);
        }
    }

}
