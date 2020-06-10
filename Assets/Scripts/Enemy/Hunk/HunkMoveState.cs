using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HunkMoveState : BaseState
{

    private Hunk hunk;
    private float waitTime = 0;

    public HunkMoveState(Hunk hunk)
    {
        this.hunk = hunk;
    }

    public override void execute()
    {
        if (Math.Abs(hunk.transform.position.x - hunk.movePos.position.x) > 0.1)
        {
            // 如果当前位置不在随机位置，则进行移动
            if (hunk.transform.position.x < hunk.movePos.position.x)
            {
                hunk.transform.localRotation = Quaternion.Euler(new Vector3(hunk.transform.rotation.x, 180, hunk.transform.rotation.z));
            }
            else
            {
                hunk.transform.localRotation = Quaternion.Euler(new Vector3(hunk.transform.rotation.x, 0, hunk.transform.rotation.z));
            }
            hunk.transform.position = Vector3.MoveTowards(hunk.transform.position, hunk.movePos.position, hunk.runSpeed * Time.deltaTime);
            hunk.anim.SetFloat("HorizontalSpeed", 1);
        }
        else
        {
            // 如果当前位置在随机位置，则开始倒计时，倒计时结束后生成新的随机位置，执行下一次移动
            hunk.anim.SetFloat("HorizontalSpeed", 0);
            if (waitTime <= 0)
            {
                hunk.movePos.position = GetRandomPos();
                waitTime = hunk.runWaitTime;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }
    }

    public override bool onEndState()
    {
        hunk.movePos.position = hunk.transform.position;
        hunk.anim.SetFloat("HorizontalSpeed", 0);
        return true;
    }

    Vector3 GetRandomPos()
    {
        Vector3 randomPos = new Vector3(UnityEngine.Random.Range(hunk.leftMovePos.position.x, hunk.rightMovePos.position.x), hunk.transform.position.y, hunk.transform.position.z);
        return randomPos;
    }

}
