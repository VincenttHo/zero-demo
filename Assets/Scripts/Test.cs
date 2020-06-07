using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            //transform.localPosition = new Vector3(transform.localPosition.x + 1, transform.localPosition.y, transform.localPosition.z);
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, 
                new Vector3(transform.localPosition.x + 1, transform.localPosition.y, transform.localPosition.z), 
                10f * Time.deltaTime);
        }
    }

}
