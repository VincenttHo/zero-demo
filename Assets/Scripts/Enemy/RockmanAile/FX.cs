

using System;
using UnityEngine;


public class FX : RockmanAile
{
    private int attackStartIndex;

    public Transform[] attackPoses1; 
    public Transform[] attackPoses2;

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
            if(transform.position.x != middleX)
            {
                if (transform.position.x < middleX)
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

        if (step == 1 && Math.Abs(transform.position.x - middleX) <= 0.5f)
        {
            step = 2;
            EndDash();
        }

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
        Invoke("EndStep", 4f);
    }

    public void InitFire()
    {
        Transform[] currentFirePoses = attackStartIndex == 1 ? attackPoses1 : attackPoses2;
        for(int n = 0; n < currentFirePoses.Length; n++)
        {
            var newFire = Instantiate(fire);
            newFire.transform.position = currentFirePoses[n].position;
        }
        Invoke("NextFire", nextFireSec);
    }

    public void NextFire()
    {
        Transform[] currentFirePoses = attackStartIndex == 1 ? attackPoses2 : attackPoses1;
        for (int n = 0; n < currentFirePoses.Length; n++)
        {
            var newFire = Instantiate(fire);
            newFire.transform.position = currentFirePoses[n].position;
        }
    }

    private void EndStep()
    {
        step = 3;
    }

}
