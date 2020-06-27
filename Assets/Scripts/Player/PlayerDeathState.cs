using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeathState : BaseState
{

    private PlayerZero playerZero;

    public PlayerDeathState(PlayerZero playerZero)
    {
        this.playerZero = playerZero;
        stateName = "death";
        playerZero.rigi.velocity = new Vector2(0, 0);
        playerZero.canControll = false;
    }

    public override void execute()
    {
    }

    public override bool onEndState()
    {
        return true;
    }

    IEnumerator Dead()
    {
        Vector3 initDir = playerZero.transform.up;
        Quaternion rotateQuate = Quaternion.AngleAxis(45, Vector3.forward);

        for (int n = 0; n < 2; n++)
        {
            for(int i = 0; i < 8; n++)
            {
                CreateEffect(initDir);
                initDir = rotateQuate * initDir;
            }

            yield return new WaitForSeconds(0.5f);
        }
    }

    void CreateEffect(Vector3 initDir)
    {
        var newEffect = GameObject.Instantiate(playerZero.playerDeadEffect);
        newEffect.transform.position = playerZero.transform.position;
        newEffect.GetComponent<PlayerDeadEffect>().moveDir = initDir;
    }
     
}
