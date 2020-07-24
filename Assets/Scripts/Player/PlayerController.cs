using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [HideInInspector]
    public static PlayerController instance;

    [HideInInspector]
    public bool inputRight;
    [HideInInspector]
    public bool inputLeft;
    [HideInInspector]
    public bool inputDown;
    [HideInInspector]
    public bool dash;
    [HideInInspector]
    public bool jump;
    [HideInInspector]
    public bool slash;
    [HideInInspector]
    public bool shoot;
    [HideInInspector]
    public bool gunCharge;

    public KeyCode upDefaultKey;
    public KeyCode downDefaultKey;
    public KeyCode leftDefaultKey;
    public KeyCode rightDefaultKey;
    public KeyCode slashDefaultKey;
    public KeyCode shootDefaultKey;
    public KeyCode jumpDefaultKey;
    public KeyCode dashDefaultKey;

    private CommonButton leftKey;
    private CommonButton rightKey;
    private CommonButton upKey;
    private CommonButton downKey;
    public CommonButton slashKey;
    private CommonButton shootKey;
    private CommonButton jumpKey;
    private CommonButton dashKey;

    public CommonButtonList keyList;

    private bool isWaitingSetup;
    private CommonButton waitingSetupKey;

    public bool isPlaying;

    private ControlSetupMenu.ResetSelectedCall resetSelectedCall;

    private string configPath;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        configPath = Application.dataPath + "/Config/control.json"; 
        RefreshButton();
    }

    private void Update()
    {
        if (GameController.instance != null && !GameController.instance.canControll && !isPlaying) return;

        // 左右移动（键盘）
        if (Input.GetKey(leftKey.GetKeyCode())) inputLeft = true;
        if (Input.GetKeyUp(leftKey.GetKeyCode())) inputLeft = false;
        if (Input.GetKey(rightKey.GetKeyCode())) inputRight = true;
        if (Input.GetKeyUp(rightKey.GetKeyCode())) inputRight = false;
        if (Input.GetKey(downKey.GetKeyCode())) inputDown = true;
        if (Input.GetKeyUp(downKey.GetKeyCode())) inputDown = false;

        // 左右移动（手柄）
        /*if (Input.GetAxis("Horizontal") < 0) inputLeft = true;
        if (Input.GetAxis("Horizontal") >= 0) inputLeft = false;
        if (Input.GetAxis("Horizontal") > 0) inputRight = true;
        if (Input.GetAxis("Horizontal") <= 0) inputRight = false;*/

        // 冲刺
        if (Input.GetKey(dashKey.GetKeyCode()) || Input.GetKey(KeyCode.Joystick1Button0)) dash = true;
        if (Input.GetKeyUp(dashKey.GetKeyCode()) || Input.GetKeyUp(KeyCode.Joystick1Button0)) dash = false;

        // 跳跃
        /*if (Input.GetKey(KeyCode.U) || Input.GetKey(KeyCode.Joystick1Button2)) jump = true;
        if (Input.GetKeyUp(KeyCode.U) || Input.GetKeyUp(KeyCode.Joystick1Button2)) jump = false;*/
        if (Input.GetKey(jumpKey.GetKeyCode()) || Input.GetKey(KeyCode.Joystick1Button2)) jump = true;
        if (Input.GetKeyUp(jumpKey.GetKeyCode()) || Input.GetKeyUp(KeyCode.Joystick1Button2)) jump = false;

        slash = false;
        if (Input.GetKeyDown(slashKey.GetKeyCode()) || Input.GetKeyDown(KeyCode.Joystick1Button5)) slash = true;

        /*shoot = false;
        if (Input.GetKeyDown(KeyCode.H)) shoot = true;*/
        shoot = false;
        if (Input.GetKey(shootKey.GetKeyCode()) || Input.GetKey(KeyCode.Joystick1Button4)) gunCharge = true;
        if (Input.GetKeyUp(shootKey.GetKeyCode()) || Input.GetKeyUp(KeyCode.Joystick1Button4))
        {
            shoot = true;
            gunCharge = false;
        }

    }

    public void ResetControl()
    {
        inputRight = false;
        inputLeft = false;
        dash = false;
        jump = false;
        slash = false;
        shoot = false;
        gunCharge = false;
    }

    public void RefreshButton()
    {
        //controlConfig = Resources.Load<TextAsset>("Config/control");
        string configJson = PlayerPrefs.GetString("PlayerController");
        if(configJson == null || configJson == "")
        {
            DefaultButton();
            //TextAsset controlConfig = Resources.Load<TextAsset>("Config/control.json");
            //configJson = controlConfig.text;
            //configJson = File.ReadAllText(configPath);
            configJson = JsonUtility.ToJson(keyList, true);
            PlayerPrefs.SetString("PlayerController", configJson);
        }
        keyList = JsonUtility.FromJson<CommonButtonList>(configJson);

        foreach (CommonButton tempButton in keyList.buttons)
        {
            switch (tempButton.GetKeyType())
            {
                case KeyType.LEFT: leftKey = tempButton; break;
                case KeyType.RIGHT: rightKey = tempButton; break;
                case KeyType.UP: upKey = tempButton; break;
                case KeyType.DOWN: downKey = tempButton; break;
                case KeyType.SLASH: slashKey = tempButton; break;
                case KeyType.SHOOT: shootKey = tempButton; break;
                case KeyType.JUMP: jumpKey = tempButton; break;
                case KeyType.DASH: dashKey = tempButton; break;
                default: break;
            }
        }
    }

    public void DefaultButton() 
    {
        /*foreach (CommonButton tempButton in keyList.buttons)
        {
            switch (tempButton.GetKeyType())
            {
                case KeyType.LEFT: tempButton.keyCodeName = leftDefaultKey.ToString(); break;
                case KeyType.RIGHT: tempButton.keyCodeName = rightDefaultKey.ToString(); break;
                case KeyType.UP: tempButton.keyCodeName = upDefaultKey.ToString(); break;
                case KeyType.DOWN: tempButton.keyCodeName = downDefaultKey.ToString(); break;
                case KeyType.SLASH: tempButton.keyCodeName = slashDefaultKey.ToString(); break;
                case KeyType.SHOOT: tempButton.keyCodeName = shootDefaultKey.ToString(); break;
                case KeyType.JUMP: tempButton.keyCodeName = jumpDefaultKey.ToString(); break;
                case KeyType.DASH: tempButton.keyCodeName = dashDefaultKey.ToString(); break;
                default: break;
            }
        }*/
        keyList = new CommonButtonList();
        List<CommonButton> buttons = new List<CommonButton>();
        buttons.Add(new CommonButton(leftDefaultKey.ToString(), KeyType.LEFT.ToString()));
        buttons.Add(new CommonButton(rightDefaultKey.ToString(), KeyType.RIGHT.ToString()));
        buttons.Add(new CommonButton(upDefaultKey.ToString(), KeyType.UP.ToString()));
        buttons.Add(new CommonButton(downDefaultKey.ToString(), KeyType.DOWN.ToString()));
        buttons.Add(new CommonButton(slashDefaultKey.ToString(), KeyType.SLASH.ToString()));
        buttons.Add(new CommonButton(shootDefaultKey.ToString(), KeyType.SHOOT.ToString()));
        buttons.Add(new CommonButton(jumpDefaultKey.ToString(), KeyType.JUMP.ToString()));
        buttons.Add(new CommonButton(dashDefaultKey.ToString(), KeyType.DASH.ToString()));
        keyList.buttons = buttons;
    }


    public void SetupButton(KeyType keyType, ControlSetupMenu.ResetSelectedCall resetSelectedCall)
    {
        isWaitingSetup = true;
        foreach (CommonButton commonButton in keyList.buttons)
        {
            if (keyType == commonButton.GetKeyType())
            {
                waitingSetupKey = commonButton;
                this.resetSelectedCall = resetSelectedCall;
                break;
            }
        }
    }

    private void OnGUI()
    {
        if (isWaitingSetup)
        {
            Event e = Event.current;
            if (e.isKey && e.keyCode != KeyCode.Space && e.keyCode != KeyCode.Return)
            {
                waitingSetupKey.keyCodeName = e.keyCode.ToString();
                isWaitingSetup = false;
                resetSelectedCall();

                // 校验冲突
                ValidClash();

            }
        }
    }

    public void SaveConfig()
    {
        // 保存到json
        string newData = JsonUtility.ToJson(keyList, true);
        //File.WriteAllText(configPath, newData);

        // 保存到临时
        PlayerPrefs.SetString("PlayerController", newData);
        RefreshButton();
    }


    private void ValidClash()
    {
        foreach (CommonButton button in keyList.buttons)
        {
            button.isClash = false;
            foreach(CommonButton button1 in keyList.buttons)
            {
                if(button != button1 && button.GetKeyCode() == button1.GetKeyCode())
                {
                    button.isClash = true;
                    break;
                }
            }
        }
    }


}
