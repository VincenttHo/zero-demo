

using System;
using UnityEngine;


public class PX : RockmanAile
{
    public GameObject kunai;

    public float waitSec;

    public Transform bulletPos;
    private PlayerZero zero;

    void Start()
    {
        base.Start();
        modelName = RockmanAileController.Model.PX;
        if (player == null) return;
        zero = player.GetComponent<PlayerZero>();
    }

    public void Update()
    {
        base.Update();
        if (!canAction) return;
        Step1Move();

        if(step == 2)
        {
            Attack();
            step = 2.5f;
        }

        if(step == 3)
        {
            canAction = false;
            controller.ChangeModel();
        }

    }

    void Attack()
    {
        anim.SetTrigger("attack");
        Invoke("InitKunai", 0.2f);
        Invoke("EndStep", waitSec);
    }

    void EndStep()
    {
        step = 3;
    }

    public void InitKunai()
    {
        var newKunai = GameObject.Instantiate(kunai);
        newKunai.transform.position = bulletPos.position;
        newKunai.transform.rotation = bulletPos.rotation;
    }

}
