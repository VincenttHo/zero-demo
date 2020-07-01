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
        //bat.transform.position = Vector3.MoveTowards(bat.transform.position, player.transform.position, bat.speed * Time.deltaTime);
        if(bat.speed > 0)
        {
            Vector2 dir = (player.transform.position + new Vector3(0, 1, 0) - bat.transform.position);
            dir = new Vector2(dir.x > 0 ? 1 : -1, dir.y > 0 ? 1 : -1);
            bat.rigi.velocity = dir * bat.speed;
        }
    }

    public override bool onEndState()
    {
        return true;
    }

}
