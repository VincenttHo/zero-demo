

using System;
using UnityEngine;


public class FX : RockmanAile
{
    private int attackStartIndex;

    public Transform[] attackPoses;

    public GameObject fire;

    public float nextFireSec;

    void Start()
    {
        base.Start();
        modelName = RockmanAileController.Model.FX;
    }

    public void Update()
    {
        base.Update();
        if (!canAction) return;
        if(step == 1)
        {
            if(transform.position.x != 0)
            {
                if (transform.position.x < 0)
                {
                    LookRight();
                }
                else
                {
                    LookLeft();
                }
                Dash();
            }
        }

        if (step == 1 && Math.Abs(transform.position.x) <= 0.5f)
        {
            step = 2;
            EndDash();
        }

        if(step == 2)
        {
            Attack();
            step = 2.5f;
        }

    }

    void Attack()
    {
        anim.SetTrigger("attack");
    }

    public void initFire()
    {
        for(int n = attackStartIndex; n < attackPoses.Length; n += 2)
        {
            var newFire = Instantiate(fire);
            newFire.transform.position = attackPoses[n].position;
        }
        Invoke("nextFire", nextFireSec);
    }

    public void nextFire()
    {
        int nextAttackIndex = attackStartIndex == 0 ? 1 : 0;
        for (int n = nextAttackIndex; n < attackPoses.Length; n += 2)
        {
            var newFire = Instantiate(fire);
            newFire.transform.position = attackPoses[n].position;
        }
    }

}
