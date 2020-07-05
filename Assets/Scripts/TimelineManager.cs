using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimelineManager : MonoBehaviour
{

    private PlayableDirector playableDirector;

    public static TimelineManager instance;

    void Start()
    {
        playableDirector = GetComponent<PlayableDirector>();
        instance = this;
    }

    public void PlayBossStory()
    {
        playableDirector.Play();
    }
}
