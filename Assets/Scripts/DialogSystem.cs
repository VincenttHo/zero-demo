using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogSystem : MonoBehaviour
{

    [Header("UI组件")]
    public Text textLabel;
    public Image avatar;

    [Header("文本文件")]
    public TextAsset textFile;
    public int index;

    [Header("文字速度")]
    public float textSpeed;

    // 是否可以下一段对话
    private bool canNextText;
    // 是否打字
    private bool canTyping;

    public Sprite zeroAvatar;
    public Sprite soundOnlyAvatar;

    List<string> textList = new List<string>();

    void Awake()
    {
        GetTextFromFile(textFile);
        canNextText = true;
        canTyping = true;
        index = 0;
    }

    private void OnEnable()
    {
        //textLabel.text = textList[index++];
        StartCoroutine(SetTextUI());
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(canNextText)
            {
                if (index < textList.Count)
                {
                    //textLabel.text = textList[index++];
                    StartCoroutine(SetTextUI());
                }
                else
                {
                    gameObject.SetActive(false);
                    index = 0;
                }
            }
            else
            {
                canTyping = false;
            }
        }
    }

    void GetTextFromFile(TextAsset textFile)
    {
        textList.Clear();
        index = 0;

        string text = textFile.text;
        var lineData = text.Split(new char[]{'\r', '\n'}, System.StringSplitOptions.RemoveEmptyEntries);

        foreach(var line in lineData)
        {
            textList.Add(line);
        }
    }

    IEnumerator SetTextUI()
    {

        textLabel.text = "";
        canNextText = false;
        switch (textList[index])
        {
            case "Zero":
                avatar.sprite = zeroAvatar;
                index++;
                break;
            case "SoundOnly":
                avatar.sprite = soundOnlyAvatar;
                index++;
                break;
            default: break;
        }

        foreach (var textChar in textList[index])
        {
            if (!canTyping) break;
            textLabel.text += textChar;
            yield return new WaitForSeconds(textSpeed);
        }
        textLabel.text = textList[index];
        canTyping = true;
        index++;
        canNextText = true;


    }
}
