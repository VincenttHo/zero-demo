using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossDialogSystem : DialogSystem
{

    public GameObject bossWarning;

    private void OnDisable()
    {
        bossWarning.SetActive(true);
    }

}
