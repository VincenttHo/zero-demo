using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStart : MonoBehaviour
{

    public PlayerZero zero;

    public void StartGame()
    {
        SpriteRenderer spriteRenderer = zero.GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = true;
        //zero.canControll = true;
        Destroy(gameObject);
    }

}
