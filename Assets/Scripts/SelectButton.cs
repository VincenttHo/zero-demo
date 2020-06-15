using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SelectButton : MonoBehaviour
{

    private GameObject lastSelect;
    private Text[] textChildren;
    private Outline[] outlineChildren;

    void Start()
    {
        lastSelect = new GameObject();
        textChildren = transform.GetComponentsInChildren<Text>();
        outlineChildren = transform.GetComponentsInChildren<Outline>();
    }

    void Update()
    {
        ClearAll();
        if (EventSystem.current.currentSelectedGameObject == null)
        {
            EventSystem.current.SetSelectedGameObject(lastSelect);
        } else
        {
            lastSelect = EventSystem.current.currentSelectedGameObject;
        }
        Transform textObj = EventSystem.current.currentSelectedGameObject.transform.GetChild(0);
        Text text = textObj.GetComponent<Text>();
        Outline outline = textObj.GetComponent<Outline>();
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
