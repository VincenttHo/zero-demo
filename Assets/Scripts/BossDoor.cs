using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class BossDoor : Door
{

    private void Start()
    {
        base.Start();
        //canUse = false;
    }

    public void OnDoorClosed()
    {
        TimelineManager.instance.PlayBossBattleStartStory();
        //BgmManager.PlayBgm(BgmManager.bossStoryBgm);
    }

}
