using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellMove : MonoBehaviour
{
    private float speed = 1;
    void Update()
    {
        transform.Translate(0, (speed * Time.deltaTime) / 2.0f, speed * Time.deltaTime);
    }
}
