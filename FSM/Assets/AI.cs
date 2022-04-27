using System;
using UnityEngine;
using UnityEngine.AI;

namespace DefaultNamespace
{
    public class AI : MonoBehaviour
    {
        private NavMeshAgent agent;
        private Animator anim;
        [SerializeField] private Transform player;
        private State currentState;

        private void Start()
        {
            agent = GetComponent<NavMeshAgent>();
            anim = GetComponent<Animator>();
            currentState = new Idle(gameObject, agent, anim, player);
        }

        private void Update()
        {
            currentState = currentState.Process();
        }
    }
}