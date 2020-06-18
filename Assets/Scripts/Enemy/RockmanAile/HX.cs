

using System;
using UnityEngine;


public class HX : RockmanAile
{

    public GameObject cyclone;
    public float waitSec;

    void Start()
    {
        base.Start();
        modelName = RockmanAileController.Model.HX;
    }

    public void Update()
    {
        base.Update();
        if (player == null) return;
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
        Invoke("EndStep", waitSec);
    }

    public void InitCyclone()
    {
        var newCyclone = GameObject.Instantiate(cyclone);
        newCyclone.transform.position = transform.position;
        newCyclone.transform.rotation = transform.rotation;
    }

    private void EndStep()
    {
        step = 3;
    }

}
