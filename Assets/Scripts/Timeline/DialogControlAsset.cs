using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

[System.Serializable]
public class DialogControlAsset : PlayableAsset
{
    public TextAsset sourceText;

    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        var playable = ScriptPlayable<DialogControlBehaviour>.Create(graph);

        var dialogControlBehaviour = playable.GetBehaviour();
        dialogControlBehaviour.sourceText = sourceText;


        return playable;
    }
}
