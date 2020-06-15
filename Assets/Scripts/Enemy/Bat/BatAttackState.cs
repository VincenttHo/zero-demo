using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatAttackState : BaseState
{

    private Bat bat;

    private GameObject player;

    public BatAttackState(Bat bat)
    {
        this.bat = bat;
        player = GameObject.FindGameObjectWithTag("Player");
        stateName = "attack";
    }

    public override void execute()
    {
        bat.transform.position = Vector3.MoveTowards(bat.transform.position, player.transform.position, bat.speed * Time.deltaTime);
    }

    public override bool onEndState()
    {
        return true;
    }

}
