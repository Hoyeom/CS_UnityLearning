using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour
{
    [SerializeField] private GameObject goal;
    [SerializeField] private NavMeshAgent agent;


    private void OnValidate()
    {
        agent ??= GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        agent.SetDestination(goal.transform.position);
    }
}
