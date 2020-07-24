using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

    public static GameController instance;

    public PlayerZero playerZero;
    private Vector3 playerPos;
    public GameObject playerDeadEffect;
    private bool isDead;
    public GameObject gameOverUI;
    public Transform gameOverUIPos;
    public GameObject dialog;

    private bool gameRunning;
    public bool canControll;
    private bool canStartUI;

    public Transform playerBossBattlePos;
    public Transform bossBossBattlePos;

    public GameObject gameStartUI;

    public Animator ScreenChangeAnim;

    public GameObject humanAile;
    public GameObject rockmanZx;

    public GameObject ctrlSetupWin;

    private void Start()
    {
        instance = this;
        canStartUI = true;


        canControll = true;
    }

    private void Update()
    {
        ShowCtrlSetupWin();
        if (gameRunning && !dialog.activeSelf && canStartUI)
        {
            canStartUI = false;
            gameStartUI.SetActive(true);
        }

        if(playerZero != null && playerZero.hp <= 0)
        {
            playerZero.rigi.velocity = new Vector2(0, 0);
            playerZero.rigi.gravityScale = 0;
            if (!isDead)
            {
                isDead = true;
                playerZero.canControll = false;
                playerPos = playerZero.transform.position;
                Invoke("PlayerDoDead", 0.5f);
            }

        }
    }

    private void PlayerDoDead()
    {
        BgmManager.StopBgm();
        SoundManager.PlayAudio(SoundManager.dead);
        StartCoroutine(Dead(playerPos, true));
        Destroy(playerZero.gameObject);
    }

    public void RockmanAileDoDead(Vector3 pos)
    {
        BgmManager.StopBgm();
        SoundManager.PlayAudio(SoundManager.dead);
        GameObject boss = GameObject.FindGameObjectWithTag("Boss");
        if(boss != null)
        {
            StartCoroutine(Dead(pos, false));
            Destroy(boss);
        }
    }

    IEnumerator Dead(Vector3 pos, bool isPlayer)
    {
        Vector3 initDir = transform.up;
        Quaternion rotateQuate = Quaternion.AngleAxis(45, Vector3.forward);

        for (int n = 0; n < 2; n++)
        {
            for (int i = 0; i < 8; i++)
            {
                CreateDeadEffect(initDir, pos);
                initDir = rotateQuate * initDir;
            }

            yield return new WaitForSeconds(0.5f);
        }

        if(isPlayer)
        {
            StartCoroutine(ShowGameOver());
        }
        else
        {
            TimelineManager.instance.PlayBossDeadStory();
        }
    }

    void CreateDeadEffect(Vector3 initDir, Vector3 pos)
    {
        var newEffect = GameObject.Instantiate(playerDeadEffect);
        newEffect.transform.position = pos + transform.up * 0.5f;
        newEffect.GetComponent<PlayerDeadEffect>().moveDir = initDir;
    }

    IEnumerator ShowGameOver()
    {
        SoundManager.audioSource.PlayOneShot(SoundManager.missionfail);
        gameOverUI = Instantiate(gameOverUI);
        gameOverUI.transform.position = new Vector3(gameOverUIPos.position.x, gameOverUIPos.position.y, -1);
        yield return new WaitForSeconds(3.5f);
        StartCoroutine(ShowReStartUI());
    }

    IEnumerator ShowReStartUI()
    {
        Destroy(gameOverUI);
        SceneManager.LoadScene(0);
        yield return 0;
    }

    // 开始
    public void StartGame()
    {
        dialog.SetActive(true);
        gameRunning = true;
    }

    public void ResetBossBattlePos()
    {
        playerZero.transform.position = playerBossBattlePos.position;
        GameObject boss = GameObject.FindGameObjectWithTag("Boss");
        boss.transform.position = bossBossBattlePos.position;
    }

    public void ScreenChange()
    {
        ScreenChangeAnim.SetTrigger("play");
    }

    public void BossChange()
    {
        var newObj = Instantiate(rockmanZx);
        newObj.transform.position = humanAile.transform.position;
        Destroy(humanAile);
    }

    private void ShowCtrlSetupWin()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && !ctrlSetupWin.activeSelf)
        {
            Time.timeScale = 0f;
            canControll = false;
            ctrlSetupWin.SetActive(true);
        }
    }

}
