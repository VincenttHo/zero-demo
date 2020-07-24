using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SelectButton : MonoBehaviour
{

    private GameObject lastSelect;
    private Text[] textChildren;

    void OnEnable()
    {
        Button currentButton = GetComponentInChildren<Button>();
        EventSystem.current.SetSelectedGameObject(currentButton.gameObject);
    }

    void Start()
    {
        lastSelect = new GameObject();
        textChildren = transform.GetComponentsInChildren<Text>();
    }

    void Update()
    {
        ClearAll();
        if (EventSystem.current.currentSelectedGameObject == null)
        {
            EventSystem.current.SetSelectedGameObject(lastSelect);
        }
        else
        {
            lastSelect = EventSystem.current.currentSelectedGameObject;
        }
        Transform textObj = EventSystem.current.currentSelectedGameObject.transform.GetChild(0);
        Text text = textObj.GetComponent<Text>();
        text.color = Color.white;
    }

    void ClearAll()
    {
        foreach(Text text in textChildren)
        {
            text.color = Color.gray;
        }
    }
}
