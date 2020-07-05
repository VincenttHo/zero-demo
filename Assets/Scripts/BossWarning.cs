using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWarning : MonoBehaviour
{

    private Boss boss;
    public GameObject bossHpBar;

    private void OnEnable()
    {
        BgmManager.PlayOneTimeBgm(BgmManager.warningBgm);
        boss = GameObject.FindGameObjectWithTag("Boss").GetComponent<Boss>();
    }

    private void OnDisable()
    {
        BgmManager.PlayBgm(BgmManager.bossBgm);
        GameController.instance.canControll = true;
        boss.canMove = true;
        bossHpBar.SetActive(true);
        HumanAileAudioManager.instance.PlayAudio(HumanAileAudioManager.bossBattleStartAudio);
    }

    void DisableObject()
    {
        gameObject.SetActive(false);
    }

}
