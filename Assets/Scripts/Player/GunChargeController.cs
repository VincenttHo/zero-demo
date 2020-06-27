using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunChargeController : MonoBehaviour
{

    private PlayerZero zero;
    private Animator anim;
    private AudioSource audioSource;
    private bool isPlayingAudio;

    void Start()
    {
        anim = GetComponent<Animator>();
        zero = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerZero>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        anim.SetInteger("lv", zero.gunChargeLv);
        if(!isPlayingAudio && zero.gunChargeLv > 0)
        {
            isPlayingAudio = true;
            audioSource.Play();
        }
        else if(zero.gunChargeLv == 0)
        {
            isPlayingAudio = false;
            audioSource.Stop();
        }
    }
}
