using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStart : MonoBehaviour
{

    private PlayerZero zero;
    private AudioSource audioSource;

    /*private void Start()
    {
        zero = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerZero>();
        audioSource = GetComponent<AudioSource>();
    }*/

    private void OnEnable()
    {
        zero = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerZero>();
        audioSource = GetComponent<AudioSource>();
        audioSource.Play();
        BgmManager.StopBgm();
        BgmManager.PlayBgm(BgmManager.stage1Bgm);
    }

    public void StartGame()
    {
        zero.canControll = true;
        GameController.instance.canControll = true;
        Destroy(gameObject);
    }

}
