using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeadEffect : MonoBehaviour
{

    public Vector3 moveDir;
    public float speed = 0.1f;

    private void Update()
    {
        transform.Translate(moveDir * speed);
    }

}
