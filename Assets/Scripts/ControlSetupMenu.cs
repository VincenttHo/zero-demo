using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ControlSetupMenu : MonoBehaviour
{

    private GameObject lastSelect;

    private Button[] buttons;
    private bool isSelected;

    public GameObject mainMenu;

    public delegate void ResetSelectedCall();

    void OnEnable()
    {
        Button currentButton = GetComponentInChildren<Button>();
        EventSystem.current.SetSelectedGameObject(currentButton.gameObject);
    }

    void Start()
    {
        lastSelect = new GameObject();
        buttons = GetComponentsInChildren<Button>();
    }

    void Update()
    {
        if (isSelected)
        {
            EventSystem.current.SetSelectedGameObject(lastSelect);
            return;
        }

        if (EventSystem.current.currentSelectedGameObject == null)
        {
            EventSystem.current.SetSelectedGameObject(lastSelect);
        }
        else
        {
            lastSelect = EventSystem.current.currentSelectedGameObject;
        }
    }

    private void OnDisable()
    {
        Time.timeScale = 1f;
        if(GameController.instance != null)
        {
            GameController.instance.canControll = true;
        }
    }

    public void SetupKey(int keyType)
    {
        if (!isSelected)
        {
            GameObject clickObj = EventSystem.current.currentSelectedGameObject;
            Animator anim = clickObj.GetComponent<Animator>();
            anim.SetTrigger("Press");
            isSelected = true;
            ResetSelectedCall call = ResetSelected;
            PlayerController.instance.SetupButton((KeyType)keyType, call);
            //PlayerController.instance.Test(call);
        }

    }

    public void ResetSelected()
    {
        isSelected = false;
        GameObject clickObj = EventSystem.current.currentSelectedGameObject;
        Animator anim = clickObj.GetComponent<Animator>();
        anim.SetTrigger("FinishSetup");
    }

    public void SaveSetup()
    {
        PlayerController.instance.SaveConfig();
        foreach (CommonButton button in PlayerController.instance.keyList.buttons)
        {
            if (button.isClash) return;
        }

        if(mainMenu != null)
        {
            mainMenu.SetActive(true);
        }
        gameObject.SetActive(false);
    }

}
