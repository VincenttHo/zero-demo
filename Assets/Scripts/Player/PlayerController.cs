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
        if (Input.GetKey(KeyCode.S) /*|| Input.GetAxis("Horizontal") < 0*/) inputLeft = true;
        if (Input.GetKeyUp(KeyCode.S) /*|| Input.GetAxis("Horizontal") >= 0*/) inputLeft = false;

        // 右移动
        if (Input.GetKey(KeyCode.F) /*|| Input.GetAxis("Horizontal") > 0*/) inputRight = true;
        if (Input.GetKeyUp(KeyCode.F) /*|| Input.GetAxis("Horizontal") <= 0*/) inputRight = false;

        // 冲刺
        if (Input.GetKey(KeyCode.I) || Input.GetKey(KeyCode.Joystick1Button0)) dash = true;
        if (Input.GetKeyUp(KeyCode.I) || Input.GetKeyUp(KeyCode.Joystick1Button0)) dash = false;

        // 跳跃
        if (Input.GetKey(KeyCode.U) || Input.GetKey(KeyCode.Joystick1Button2)) jump = true;
        if (Input.GetKeyUp(KeyCode.U) || Input.GetKeyUp(KeyCode.Joystick1Button2)) jump = false;

        slash = false;
        if (Input.GetKeyDown(KeyCode.Y) || Input.GetKeyDown(KeyCode.Joystick1Button5)) slash = true;

        /*shoot = false;
        if (Input.GetKeyDown(KeyCode.H)) shoot = true;*/
        shoot = false;
        if (Input.GetKey(KeyCode.H) || Input.GetKey(KeyCode.Joystick1Button4)) gunCharge = true;
        if (Input.GetKeyUp(KeyCode.H) || Input.GetKeyUp(KeyCode.Joystick1Button4))
        {
            shoot = true;
            gunCharge = false;
        }

    }

}
