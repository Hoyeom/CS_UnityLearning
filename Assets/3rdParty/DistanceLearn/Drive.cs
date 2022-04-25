using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;
using UnityEngine.UI;

// A very simplistic car driving on the x-z plane.

public class Drive : MonoBehaviour
{
    public float speed = 10.0f;
    public float rotationSpeed = 100.0f;

    public GameObject target;
    private bool autoPilot = false;
    
    void Start()
    {
        
    }

    float CalculateDistance()
    {
        Vector3 tankPos = transform.position;
        Vector3 targetPos = target.transform.position;

        float distance =
            Mathf.Sqrt(
                Mathf.Pow(tankPos.x - targetPos.x, 2) +
                Mathf.Pow(tankPos.y - targetPos.y, 2)) +
            Mathf.Pow(tankPos.z - targetPos.z, 2);

        float unityDistance = Vector2.Distance(tankPos, targetPos);

        Debug.Log($"Distance: {distance}");
        Debug.Log($"UnityDistance: {unityDistance}");

        return distance;
    }

    void CalculateAngle()
    {
        Vector3 tankForward = this.transform.up;
        Vector3 targetDir = (target.transform.position - transform.position);

        float dot = (tankForward.x * targetDir.x + tankForward.y * targetDir.y);
        float angle = Mathf.Acos(dot / (tankForward.magnitude * targetDir.magnitude));

        Debug.Log($"Angle: {angle * Mathf.Rad2Deg}");
        Debug.Log($"UnityAngle: {Vector3.Angle(tankForward,targetDir)}");
 
        Debug.DrawRay(transform.position, tankForward * targetDir.magnitude, Color.green, 2f);
        Debug.DrawRay(transform.position, targetDir, Color.red, 2f);

        int clockwise = 1;
        if (Cross(tankForward, targetDir).z < 0)
            clockwise = -1;

        float unityAngle = Vector3.SignedAngle(tankForward, targetDir, transform.forward);

        this.transform.Rotate(0, 0, (angle * clockwise * Mathf.Rad2Deg) * Time.deltaTime);
        // this.transform.Rotate(0, 0, unityAngle);
    }
    
    Vector3 Cross(Vector3 v, Vector3 w)
    {
        float xMult = v.y * w.z - v.z * w.y;
        float yMult = v.z * w.x - v.x * w.z;
        float zMult = v.x * w.y - v.y * w.x;
        
        Vector3 crossProd = new Vector3(xMult, yMult, zMult);
        return crossProd;
    }

    private float autoSpeed = 0.1f;

    void AutoPilot()
    {
        CalculateAngle();
        transform.Translate(Vector3.up * autoSpeed);
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
            CalculateAngle();
        }

        if (Keyboard.current.tKey.wasPressedThisFrame)
        {
            autoPilot = !autoPilot;
        }

        if (autoPilot)
        {
            if(CalculateDistance()>5)
                AutoPilot();
        }
    }
}