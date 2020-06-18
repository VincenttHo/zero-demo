

using System;
using UnityEngine;


public class LX : RockmanAile
{
    public GameObject dragon;

    public float waitSec;

    public Transform[] attackPoses1;
    public Transform[] attackPoses2;

    private int attackStartIndex;

    public float nextDrgonSec;

    void Start()
    {
        base.Start();
        modelName = RockmanAileController.Model.LX;
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
        Invoke("EndStep", 4f);
    }

    void EndStep()
    {
        step = 3;
    }

    public void InitDrgon()
    {
        Transform[] currentDrgonPoses = attackStartIndex == 1 ? attackPoses1 : attackPoses2;
        for (int n = 0; n < currentDrgonPoses.Length; n++)
        {
            var newDrgon = Instantiate(dragon);
            newDrgon.transform.position = currentDrgonPoses[n].position;
        }
        Invoke("NextDrgon", nextDrgonSec);
    }

    public void NextDrgon()
    {
        Transform[] currentDrgonPoses = attackStartIndex == 1 ? attackPoses2 : attackPoses1;
        for (int n = 0; n < currentDrgonPoses.Length; n++)
        {
            var newDrgon = Instantiate(dragon);
            newDrgon.transform.position = currentDrgonPoses[n].position;
        }
    }

    public void EndDashAttack()
    {

    }

}
