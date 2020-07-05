using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class BossDoor : Door
{

    public void OnDoorClosed()
    {
        TimelineManager.instance.PlayBossStory();
        BgmManager.PlayBgm(BgmManager.bossStoryBgm);
    }

}
