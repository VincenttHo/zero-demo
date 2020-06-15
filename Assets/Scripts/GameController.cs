using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

    public PlayerZero playerZero;
    private Vector3 playerPos;
    public GameObject playerDeadEffect;
    private bool isDead;
    public GameObject gameOverUI;
    public Transform gameOverUIPos;

    private void Update()
    {
        if(playerZero != null && playerZero.hp <= 0)
        {
            playerZero.rigi.velocity = new Vector2(0, 0);
            playerZero.rigi.gravityScale = 0;
            if (!isDead)
            {
                isDead = true;
                playerZero.canControll = false;
                playerPos = playerZero.transform.position;
                Invoke("DoDead", 0.5f);
            }

        }
    }

    private void DoDead()
    {
        Destroy(playerZero.gameObject);
        StartCoroutine(PlayerDead());
    }

    IEnumerator PlayerDead()
    {
        Vector3 initDir = transform.up;
        Quaternion rotateQuate = Quaternion.AngleAxis(45, Vector3.forward);

        for (int n = 0; n < 2; n++)
        {
            for (int i = 0; i < 8; i++)
            {
                CreateDeadEffect(initDir);
                initDir = rotateQuate * initDir;
            }

            yield return new WaitForSeconds(0.5f);
        }

        StartCoroutine(ShowGameOver());
    }

    void CreateDeadEffect(Vector3 initDir)
    {
        var newEffect = GameObject.Instantiate(playerDeadEffect);
        newEffect.transform.position = playerPos + transform.up * 0.5f;
        newEffect.GetComponent<PlayerDeadEffect>().moveDir = initDir;
    }

    IEnumerator ShowGameOver()
    {
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

}
