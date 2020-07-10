using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

[System.Serializable]
public class BgmControlAsset : PlayableAsset
{
    public AudioClip bgm;

    public bool stopWhenEnd;

    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        var playable = ScriptPlayable<BgmControlBehaviour>.Create(graph);

        var bgmControlBehaviour = playable.GetBehaviour();
        bgmControlBehaviour.bgm = bgm;
        bgmControlBehaviour.stopWhenEnd = stopWhenEnd;


        return playable;
    }
}
