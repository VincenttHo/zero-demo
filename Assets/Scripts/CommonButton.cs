using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CommonButton
{
    [NonSerialized]
    private KeyCode keyCode;

    [NonSerialized]
    private KeyType keyType;

    public string keyCodeName;

    public string keyTypeName;

    [NonSerialized]
    public bool isClash;

    public CommonButton(string keyCodeName, string keyTypeName)
    {
        this.keyCodeName = keyCodeName;
        this.keyTypeName = keyTypeName;
    }

    public KeyCode GetKeyCode()
    {
        if (keyCodeName != null) {
            keyCode = (KeyCode)Enum.Parse(typeof(KeyCode), keyCodeName);
        }
        return keyCode;
    }

    public KeyType GetKeyType()
    {
        if (keyTypeName != null)
        {
            keyType = (KeyType)Enum.Parse(typeof(KeyType), keyTypeName);
        }
        return keyType;
    }

}
