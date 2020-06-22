

using System;
using UnityEngine;

/**
 * 洛克人状态艾尔
 * 1.ZX状态：射击三发蓄力弹，在一定距离内会冲刺到玩家面前三连斩
 * 2.PX状态：发射一个可以在墙上多次反弹的风魔手里剑
 * 3.HX状态：在跟前放出一个有吸力的龙卷风（hp过半以后用，作为辅助技能，放了这个之后切换PX或者ZX攻击）
 * 4.LX状态：在自己的一侧放出一列冰（中间随机留一个缺口，缺口位置放一个跳台用于躲招）
 */
public static class AileHpManager
{
    public static float maxHp = 100;

    public static float currentHp = maxHp;

}
