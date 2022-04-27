using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowWayPoint : MonoBehaviour
{
    [SerializeField] private Transform[] wayPoints;
    [SerializeField] private int currentWayPoint = 0;

    public float speed = 10f;
    public float rotSpeed = 10f;
    public float lookAhead = 10f;

    private GameObject tracker;

    private void Start()
    {
        tracker = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        DestroyImmediate(tracker.GetComponent<Collider>());
        tracker.GetComponent<MeshRenderer>().enabled = false;
        tracker.transform.position = transform.position;
        tracker.transform.rotation = transform.rotation;
    }

    void ProgressTracker()
    {
        if (Vector3.Distance(tracker.transform.position, transform.position) > lookAhead) { return; }
        
        if (Vector3.Distance(tracker.transform.position, wayPoints[currentWayPoint].position) < 3)
            currentWayPoint = (currentWayPoint+1) % wayPoints.Length;
        
        tracker.transform.LookAt(wayPoints[currentWayPoint]);
        tracker.transform.Translate(0,0,(speed + 20) * Time.deltaTime);
    }

    private void Update()
    {
        ProgressTracker();
        // transform.LookAt(wayPoints[currentWayPoint]);

        Quaternion lookAtWayPoint = Quaternion.LookRotation(tracker.transform.position - transform.position);
        
        transform.rotation = Quaternion.Slerp(transform.rotation, lookAtWayPoint, rotSpeed * Time.deltaTime);
        
        transform.Translate(0,0,speed * Time.deltaTime);

    }
}