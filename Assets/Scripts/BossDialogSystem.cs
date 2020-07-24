using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class BossDialogSystem : DialogSystem
{

    public GameObject bossWarning;

    public TextAsset bossBattleStartText;
    public TextAsset bossChangeText;
    public TextAsset bossEndText;

    public static BossDialogSystem instance;

    private PlayableDirector director;

    void Awake()
    {
        base.Awake();
        instance = this;
        director = GameObject.FindGameObjectWithTag("Director").GetComponent<PlayableDirector>();
    }

    private void OnEnable()
    {
        base.OnEnable();
        director = GameObject.FindGameObjectWithTag("Director").GetComponent<PlayableDirector>();
    }

    private void OnDisable()
    {
        director.Play();
        /*if(textFile == bossChangeText)
        {
            GameController.instance.BossChange(); 
        }*/
        /*if (textFile == bossBattleStartText)
        {
            bossWarning.SetActive(true);
        }*/
    }

    public static void SetChangeText()
    {

    }

}
