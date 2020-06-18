

using System;
using UnityEngine;

/**
 * 洛克人状态艾尔
 * 1.ZX状态：射击三发蓄力弹，在一定距离内会冲刺到玩家面前三连斩
 * 2.PX状态：发射一个可以在墙上多次反弹的风魔手里剑
 * 3.HX状态：在跟前放出一个有吸力的龙卷风（hp过半以后用，作为辅助技能，放了这个之后切换PX或者ZX攻击）
 * 4.LX状态：在自己的一侧放出一列冰（中间随机留一个缺口，缺口位置放一个跳台用于躲招）
 */
public class RockmanAileController : MonoBehaviour
{
    private GameObject currentModel;
    private RockmanAile aile;

    public GameObject zxObj;
    public GameObject fxObj;
    public GameObject pxObj;
    public GameObject lxObj;
    public GameObject hxObj;

    public enum Model
    {
        ZX = 0,
        FX = 1,
        PX = 2,
        LX = 3,
        HX = 4
    }

    void Start()
    {
        currentModel = zxObj;
        aile = currentModel.GetComponent<ZX>();
    }

    void Update()
    {
    }

    public void ChangeModel()
    {
        int index = (int)aile.modelName;
        while (aile.modelName == (Model)index)
        {
            index = UnityEngine.Random.Range(0, 5);
            /*if(index == 4)
            {
                index = 0;
            }
            else
            {
                index++;
            }*/
        }
        switch ((Model)index)
        {
            case Model.ZX:
                aile.DoChange(zxObj);
                currentModel = zxObj;
                aile = currentModel.GetComponent<ZX>();
                break;
            case Model.FX:
                aile.DoChange(fxObj);
                currentModel = fxObj;
                aile = currentModel.GetComponent<FX>();
                break;
            case Model.PX:
                aile.DoChange(pxObj);
                currentModel = pxObj;
                aile = currentModel.GetComponent<PX>();
                break;
            case Model.LX:
                aile.DoChange(lxObj);
                currentModel = lxObj;
                aile = currentModel.GetComponent<LX>();
                break;
            case Model.HX:
                aile.DoChange(hxObj);
                currentModel = hxObj;
                aile = currentModel.GetComponent<HX>();
                break;
            default: break;
        }
    }

}
