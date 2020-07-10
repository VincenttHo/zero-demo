using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime.Tasks.Unity.UnityAudioSource;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

// A behaviour that is attached to a playable
public class DialogControlBehaviour : PlayableBehaviour
{
    private Image dialogBox;
    public Image avatar;
    public Sprite avatarSourceImage;
    public Text text;
    public TextAsset sourceText;

    private PlayableDirector director;

    public override void OnPlayableCreate(Playable playable)
    {
        director = playable.GetGraph().GetResolver() as PlayableDirector;
    }

    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        dialogBox = playerData as Image; // 这个地方有变化
        if (dialogBox != null)
        {
            DialogSystem dialogSys = dialogBox.gameObject.GetComponent<DialogSystem>();
            dialogSys.GetTextFromFile(sourceText);
            dialogBox.gameObject.SetActive(true);
            /*avatar = GameObject.FindGameObjectWithTag("DialogAvatar").GetComponent<Image>();
            text = GameObject.FindGameObjectWithTag("DialogText").GetComponent<Text>();
            avatar.sprite = avatarSourceImage;
            text.text = sourceText;*/
            director.Pause();

        }
    }

    /*public override void OnGraphStop(Playable playable)
    {
        director.Pause();
    }*/

}
