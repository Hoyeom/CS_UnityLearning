using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropCylinder : MonoBehaviour
{
    [SerializeField] private GameObject obstacle;
    private GameObject[] agents;
    
    void Start()
    {
        agents = GameObject.FindGameObjectsWithTag("agent");
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray.origin, ray.direction, out hit))
            {
                Instantiate(obstacle, hit.point, obstacle.transform.rotation);
                foreach (var agent in agents)
                {
                    agent.GetComponent<AIControl>().DetectNewObstacle(hit.point);
                }
            }

        }
    }
}
