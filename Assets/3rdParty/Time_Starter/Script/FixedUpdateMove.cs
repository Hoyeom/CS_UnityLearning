using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedUpdateMove : MonoBehaviour
{
    private float speed = 2.0f;
    void FixedUpdate()
    {
        transform.Translate(0, 0, Time.deltaTime * speed);
    }
}
