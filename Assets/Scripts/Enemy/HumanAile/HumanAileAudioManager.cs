using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanAileAudioManager : MonoBehaviour
{

    private AudioSource audioSource;

    public static HumanAileAudioManager instance;
    public static AudioClip bossBattleStartAudio;
    public static AudioClip deadAudio;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        bossBattleStartAudio = Resources.Load<AudioClip>("boss_start_loud");
        deadAudio = Resources.Load<AudioClip>("boss_dead");
        instance = this;
    }

    public void PlayAudio(AudioClip audio)
    {
        audioSource.clip = audio;
        audioSource.Play();
    }

}
