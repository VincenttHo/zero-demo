using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{

    private Animator anim;
    private PlayerZero zero;
    private bool isInsiding;

    protected bool canUse;

    protected void Start()
    {
        anim = GetComponent<Animator>();
        zero = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerZero>();
        canUse = true;
    }

    private void Update()
    {
        if(isInsiding)
        {
            zero.input = 1;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player") && other is CircleCollider2D && canUse)
        {
            BgmManager.StopBgm();
            canUse = false;
            isInsiding = true;
            GameController.instance.canControll = false;
            anim.SetTrigger("open");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && other is CircleCollider2D && isInsiding)
        {
            isInsiding = false;
            zero.input = 0;
            anim.SetTrigger("close");
        }
    }

    public void OnDoorClosed()
    {
        GameController.instance.canControll = true;
    }

}
