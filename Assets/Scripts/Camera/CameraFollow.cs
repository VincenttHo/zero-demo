using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 主摄像机脚本
/// </summary>
public class CameraFollow : MonoBehaviour 
{

    public Transform target;
    public float smoothing;

    public Vector2 minPos;
    public Vector2 maxPos;
    public List<PosConfig> posConfigs;
    private int index = 0;

    private void Update()
    {   if (target == null) return;
        if(!(target.position.x >= posConfigs[index].minPos.x &&
                target.position.x <= posConfigs[index].maxPos.x &&
                target.position.y >= posConfigs[index].minPos.y &&
                target.position.y <= posConfigs[index].maxPos.y))
        {
            for (int n = 0; n < posConfigs.Count; n++)
            {
                if (n == index) continue;
                if (target.position.x >= posConfigs[n].minPos.x &&
                    target.position.x <= posConfigs[n].maxPos.x &&
                    target.position.y >= posConfigs[n].minPos.y &&
                    target.position.y <= posConfigs[n].maxPos.y)
                {
                    index = n;
                    break;
                }
            }
        }
        
    }

    private void LateUpdate()
    {
        if(target != null)
        {
            if(transform.position != target.position)
            {
                Vector3 targetPos = target.position;
                targetPos.x = Mathf.Clamp(targetPos.x, posConfigs[index].minPos.x, posConfigs[index].maxPos.x);
                targetPos.y = Mathf.Clamp(targetPos.y, posConfigs[index].minPos.y, posConfigs[index].maxPos.y);
                targetPos = new Vector3(targetPos.x, targetPos.y, transform.position.z);
                transform.position = Vector3.Lerp(transform.position, targetPos, smoothing);
            }
        }
    }

}
