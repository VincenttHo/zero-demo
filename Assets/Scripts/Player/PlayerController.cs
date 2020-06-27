using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public static bool inputRight;
    public static bool inputLeft;
    public static bool dash;
    public static bool jump;
    public static bool slash;
    public static bool shoot;
    public static bool gunCharge;

    private void Update()
    {
        if (!GameController.instance.canControll) return;
        // 左移动
        if (Input.GetKey(KeyCode.S)) inputLeft = true;
        if (Input.GetKeyUp(KeyCode.S)) inputLeft = false;

        // 右移动
        if (Input.GetKey(KeyCode.F)) inputRight = true;
        if (Input.GetKeyUp(KeyCode.F)) inputRight = false;

        // 冲刺
        if (Input.GetKey(KeyCode.I)) dash = true;
        if (Input.GetKeyUp(KeyCode.I)) dash = false;

        // 跳跃
        if (Input.GetKey(KeyCode.U)) jump = true;
        if (Input.GetKeyUp(KeyCode.U)) jump = false;

        slash = false;
        if (Input.GetKeyDown(KeyCode.Y)) slash = true;

        /*shoot = false;
        if (Input.GetKeyDown(KeyCode.H)) shoot = true;*/
        shoot = false;
        if (Input.GetKey(KeyCode.H)) gunCharge = true;
        if (Input.GetKeyUp(KeyCode.H))
        {
            shoot = true;
            gunCharge = false;
        }

    }

}
