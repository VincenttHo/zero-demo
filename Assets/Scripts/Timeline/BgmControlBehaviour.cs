using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime.Tasks.Unity.UnityAudioSource;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

// A behaviour that is attached to a playable
public class BgmControlBehaviour : PlayableBehaviour
{
    public AudioClip bgm;
    public bool stopWhenEnd;

    private AudioSource audioSource;

    private PlayableDirector director;

    public override void OnPlayableCreate(Playable playable)
    {
        director = playable.GetGraph().GetResolver() as PlayableDirector;
    }

    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        audioSource = playerData as AudioSource;
        audioSource.clip = bgm;
        audioSource.loop = true;
        audioSource.Play();
    }

    public override void OnGraphStop(Playable playable)
    {
        if (stopWhenEnd && director.state != PlayState.Paused)
        {
            audioSource.Stop();
        }
    }

}
