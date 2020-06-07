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

    private void LateUpdate()
    {
        if(target != null)
        {
            if(transform.position != target.position)
            {
                Vector3 targetPos = target.position;
                targetPos.x = Mathf.Clamp(targetPos.x, minPos.x, maxPos.x);
                targetPos.y = Mathf.Clamp(targetPos.y, minPos.y, maxPos.y);
                targetPos = new Vector3(targetPos.x, targetPos.y, transform.position.z);
                transform.position = Vector3.Lerp(transform.position, targetPos, smoothing);
            }
        }
    }


}
