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

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        slash = Resources.Load<AudioClip>("slash");
        shoot = Resources.Load<AudioClip>("shoot");
        run = Resources.Load<AudioClip>("run");
        dash = Resources.Load<AudioClip>("dash");
    }

    public static void PlayAudio(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
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
