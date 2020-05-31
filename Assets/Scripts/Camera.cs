using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 主摄像机脚本
/// </summary>
public class Camera : MonoBehaviour 
{

    public Transform target;
    public Renderer background;
    public float smoothing;
    private float boundsX;
    private float boundsY;
    private BoxCollider2D collider;
    private float cameraBoundsX;
    private float cameraBoundsY;

    private void Start()
    {
        collider = GetComponentInChildren<BoxCollider2D>();
        boundsX = background.bounds.size.x / 2;
        boundsY = background.bounds.size.y / 2;
        cameraBoundsX = collider.bounds.size.x / 2;
        cameraBoundsY = collider.bounds.size.y / 2;
    }

    private void LateUpdate()
    {
        if(target != null)
        {
            if(CheckXBounds())
            {
                Vector3 playerPos = new Vector3(target.position.x, transform.position.y, transform.position.z);
                //transform.position = Vector3.Lerp(transform.position, playerPos, smoothing);
                transform.position = playerPos;
            }
            if (CheckYBounds())
            {
                Vector3 playerPos = new Vector3(transform.position.x, target.position.y, transform.position.z);
                //transform.position = Vector3.Lerp(transform.position, playerPos, smoothing);
                transform.position = playerPos;
            }
        }
    }

    private bool CheckXBounds()
    {
        bool leftCheck = target.position.x - cameraBoundsX > -boundsX;
        bool rightCheck = target.position.x + cameraBoundsX < boundsX;
        return leftCheck && rightCheck;
    }

    private bool CheckYBounds()
    {
        bool topCheck = target.position.y + cameraBoundsY < boundsY;
        bool bottomCheck = target.position.y - cameraBoundsY > -boundsY;
        return topCheck && bottomCheck;
    }

}
