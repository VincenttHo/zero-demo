using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStart : MonoBehaviour
{

    public GameObject zero;

    public void StartGame()
    {
        zero.SetActive(true);
        Destroy(gameObject);
    }

}
