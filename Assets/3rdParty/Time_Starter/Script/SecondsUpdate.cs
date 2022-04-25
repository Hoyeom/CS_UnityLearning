using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondsUpdate : MonoBehaviour
{
    private float timeStartOffset = 0;
    private bool gotStartTime = false;
    private float speed = 2.0f;
    
    void Update()
    {
        if (!gotStartTime)
        {
            timeStartOffset = Time.realtimeSinceStartup;
            gotStartTime = true;
        }

        transform.position = new Vector3(transform.position.x, transform.position.y,
            (Time.realtimeSinceStartup - timeStartOffset) * speed);
    }
}
