

using System;
using UnityEngine;


public class PX : RockmanAile
{
    public GameObject bullet;

    public float waitSec;

    void Start()
    {
        base.Start();
        modelName = RockmanAileController.Model.PX;
    }

    public void Update()
    {
        base.Update();

        Step1Move();

        if(step == 2)
        {
            Attack();
            step = 2.5f;
        }

        if(step == 3)
        {

        }

    }

    void Attack()
    {
        anim.SetTrigger("attack");
        Invoke("EndStep", waitSec);
    }

    void EndStep()
    {
        step = 3;
    }

}
