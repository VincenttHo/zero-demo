using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public static AudioSource audioSource;
    public static AudioClip slash;
    public static AudioClip shoot;
    public static AudioClip run;
    public static AudioClip dash;
    public static AudioClip dead;
    public static AudioClip hurt;
    public static AudioClip missionfail;
    public static AudioClip walljump;
    public static AudioClip slidingwall;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        slash = Resources.Load<AudioClip>("slash");
        shoot = Resources.Load<AudioClip>("shoot");
        run = Resources.Load<AudioClip>("run");
        dash = Resources.Load<AudioClip>("dash");
        dead = Resources.Load<AudioClip>("dead");
        hurt = Resources.Load<AudioClip>("hurt");
        missionfail = Resources.Load<AudioClip>("missionfail");
        walljump = Resources.Load<AudioClip>("walljump");
        slidingwall = Resources.Load<AudioClip>("slidingwall");
    }

    public static void PlayAudio(AudioClip clip)
    {
        //audioSource.PlayOneShot(clip);
        audioSource.clip = clip;
        audioSource.Play();
    }

    public static void PlayAudioLoop(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.loop = true;
        audioSource.Play();
    }

    public static void StopAudioLoop(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.loop = false;
        audioSource.Stop();
    }

}
