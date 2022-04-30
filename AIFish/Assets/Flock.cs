using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Flock : MonoBehaviour
{
    public FlockManager myManager;
    private float speed;
    private bool turning = false;

    private void Start()
    {
        speed = Random.Range(myManager.minSpeed,
            myManager.maxSpeed);
    }

    private void Update()
    {
        Bounds b = new Bounds(myManager.transform.position, myManager.swimLimits * 2);

        RaycastHit hit = new RaycastHit();
        Vector3 dir = Vector3.zero;

        if (!b.Contains(transform.position))
        {
            turning = true;
            dir = myManager.transform.position - transform.position;
        }
        else if(Physics.Raycast(transform.position,transform.forward *50,out hit))
        {
            turning = true;
            dir = Vector3.Reflect(transform.forward, hit.normal);
        }
        else
        {
            turning = false;
        }

        if (turning)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation,
                Quaternion.LookRotation(dir),
                myManager.rotationSpeed * Time.deltaTime);
        }
        else
        {
            if (Random.Range(0, 100) < 10)
                speed = Random.Range(myManager.minSpeed,
                    myManager.maxSpeed);
            if (Random.Range(0, 100) < 20)
                ApplyRule();
        }


        transform.Translate(0, 0, speed * Time.deltaTime);
    }

    void ApplyRule()
    {
        GameObject[] gos;
        gos = myManager.allFish;

        Vector3 vcentre = Vector3.zero;
        Vector3 vavoid = Vector3.zero;
        float gSpeed = 0.01f;
        float nDistance;
        int groupSize = 0;

        foreach (var go in gos)
        {
            if (go != gameObject)
            {
                nDistance = Vector3.Distance(go.transform.position, transform.position);
                if (nDistance <= myManager.neighbourDistance)
                {
                    vcentre += go.transform.position;
                    groupSize++;
                    if (nDistance < 1.0f)
                    {
                        vavoid = vavoid + (transform.position - go.transform.position);
                    }

                    Flock anotherFlock = go.GetComponent<Flock>();
                    gSpeed = gSpeed + anotherFlock.speed;
                }
            }
        }

        if (groupSize > 0)
        {
            vcentre = vcentre / groupSize + (myManager.goalPos - transform.position);
            speed = gSpeed / groupSize;

            Vector3 dir = (vcentre + vavoid) - transform.position;
            if (dir != Vector3.zero)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation,
                    Quaternion.LookRotation(dir),
                    myManager.rotationSpeed * Time.deltaTime);
            }
        }
    }
}