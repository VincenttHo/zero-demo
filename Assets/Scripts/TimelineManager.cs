using BehaviorDesigner.Runtime.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.Timeline;

public class TimelineManager : MonoBehaviour
{

    private PlayableDirector playableDirector;

    public TimelineAsset bossBattleStart;
    public TimelineAsset bossChange;
    public TimelineAsset bossDead;

    public static TimelineManager instance;

    private GameObject bossObj;
    private Boss boss;

    public delegate void TestDelegate();
    public event Action<PlayerZero> testEvent;

    void Start()
    {
        playableDirector = GetComponent<PlayableDirector>();
        instance = this;

        // 播放状态回调
        playableDirector.played += OnPlaying;
        playableDirector.stopped += OnStopping;
    }


    public void PlayBossBattleStartStory()
    {
        playableDirector.playableAsset = bossBattleStart;
        playableDirector.Play();
    }

    public void PlayBossChangeStory()
    {
        playableDirector.playableAsset = bossChange;
        playableDirector.Play();
    }

    public void PlayBossDeadStory()
    {
        playableDirector.playableAsset = bossDead;
        playableDirector.Play();
    }

    void OnPlaying(PlayableDirector director)
    {
        bossObj = GameObject.FindGameObjectWithTag("Boss");
        if (bossObj == null) return;
        boss = bossObj.GetComponent<Boss>();
        GameController.instance.canControll = false;
        PlayerController.instance.ResetControl();
        boss.canMove = false;
    }

    void OnStopping(PlayableDirector director)
    {
        if(director.playableAsset == bossDead)
        {
            SceneManager.LoadScene(0);
        }

        bossObj = GameObject.FindGameObjectWithTag("Boss");
        if (bossObj == null) return;
        boss = bossObj.GetComponent<Boss>();
        GameController.instance.canControll = true;
        boss.canMove = true;

    }

}
