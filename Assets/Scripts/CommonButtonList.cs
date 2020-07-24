using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CommonButtonList
{

    public List<CommonButton> buttons;

    public CommonButton GetByKeyType(KeyType keyType)
    {
        foreach(CommonButton button in buttons) 
        {
            if(button.GetKeyType() == keyType)
            {
                return button;
            }
        }
        return null;
    }

}
