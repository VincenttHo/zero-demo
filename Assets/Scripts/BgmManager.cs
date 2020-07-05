using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgmManager : MonoBehaviour
{
    private static AudioSource audioSource;
    public static AudioClip missionStartBgm;
    public static AudioClip stage1Bgm;
    public static AudioClip bossBgm;
    public static AudioClip bossStoryBgm;
    public static AudioClip warningBgm;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        missionStartBgm = Resources.Load<AudioClip>("mission_start_bgm");
        stage1Bgm = Resources.Load<AudioClip>("stage1_bgm");
        bossBgm = Resources.Load<AudioClip>("boss");
        bossStoryBgm = Resources.Load<AudioClip>("boss_story");
        warningBgm = Resources.Load<AudioClip>("warning");
    }

    public static void PlayBgm(AudioClip clip)
    {
        audioSource.loop = true;
        audioSource.clip = clip;
        audioSource.Play();
    }

    public static void StopBgm()
    {
        audioSource.Stop();
    }

    public static void PlayOneTimeBgm(AudioClip clip)
    {
        audioSource.loop = false;
        audioSource.clip = clip;
        audioSource.Play();
    }

}
