using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{

    public Animator cameraShakeAnim;

    public void shake()
    {
        cameraShakeAnim.SetTrigger("shake");
    }

}
