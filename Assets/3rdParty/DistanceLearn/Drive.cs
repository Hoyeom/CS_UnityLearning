using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;
using UnityEngine.UI;

// A very simplistic car driving on the x-z plane.

public class Drive : MonoBehaviour
{
    public float speed = 10.0f;
    public float rotationSpeed = 100.0f;

    public GameObject fuel;
    
    void Start()
    {
        
    }

    void CalculateDistance()
    {
        Vector3 tankPos = transform.position;
        Vector3 targetPos = fuel.transform.position;

        float distance =
            Mathf.Sqrt(
                Mathf.Pow(tankPos.x - targetPos.x, 2) +
                Mathf.Pow(tankPos.y - targetPos.y, 2)) +
            Mathf.Pow(tankPos.z - targetPos.z, 2);

        float unityDistance = Vector2.Distance(tankPos, targetPos);

        Debug.Log($"Distance: {distance}");
        Debug.Log($"UnityDistance: {unityDistance}");
    }

    void Update()
    {
        // Get the horizontal and vertical axis.
        // By default they are mapped to the arrow keys.
        // The value is in the range -1 to 1
        float translation = Input.GetAxis("Vertical") * speed;
        float rotation = Input.GetAxis("Horizontal") * rotationSpeed;

        // Make it move 10 meters per second instead of 10 meters per frame...
        translation *= Time.deltaTime;
        rotation *= Time.deltaTime;

        // Move translation along the object's z-axis
        transform.Translate(0, translation, 0);

        // Rotate around our y-axis
        transform.Rotate(0, 0, -rotation);

        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            CalculateDistance();
        }

    }
}