using System;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class AIControl : MonoBehaviour {

    GameObject[] goalLocations;
    NavMeshAgent agent;
    Animator anim;
    private float speedMult;
    private float detectionRadius = 20;
    private float fleeRadius = 10;

    void ResetAgent()
    {
        speedMult = Random.Range(0.1f, 1.5f);
        agent.speed = 2 * speedMult;
        agent.angularSpeed = 120;
        anim.SetFloat("speedMult", speedMult);
        anim.SetTrigger("isWalking");
        agent.ResetPath();
    }
    
    public void DetectNewObstacle(Vector3 pos)
    {
        if (Vector3.Distance(pos, transform.position) < detectionRadius)
        {
            Vector3 fleeDir = (transform.position - pos).normalized;
            Vector3 newGoal = transform.position + fleeDir * fleeRadius;

            NavMeshPath path = new NavMeshPath();
            agent.CalculatePath(newGoal, path);

            if (path.status != NavMeshPathStatus.PathInvalid)
            {
                agent.SetDestination(path.corners[path.corners.Length - 1]);
                anim.SetTrigger("isRunning");
                agent.speed = 10;
                agent.angularSpeed = 500;
            }
        }
    }


    private void Start()
    {
        goalLocations = GameObject.FindGameObjectsWithTag("goal");
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(goalLocations[Random.Range(0, goalLocations.Length)].transform.position);
        anim = GetComponent<Animator>();
        anim.SetFloat("wOffset",Random.Range(0f,1f));
        ResetAgent();
    }

    private void Update()
    {
        if (agent.remainingDistance < agent.stoppingDistance)
        {
            ResetAgent();
            agent.SetDestination(goalLocations[Random.Range(0, goalLocations.Length)].transform.position);
        }
    }


}

// Use this for initialization
/*void Start() {
    goalLocations = GameObject.FindGameObjectsWithTag("goal");
    agent = this.GetComponent<UnityEngine.AI.NavMeshAgent>();
    agent.SetDestination(goalLocations[Random.Range(0, goalLocations.Length)].transform.position);
    anim = this.GetComponent<Animator>();
    anim.SetFloat("wOffset", Random.Range(0.0f, 1.0f));
    anim.SetTrigger("isWalking");
    float sm = Random.Range(0.1f, 1.5f);
    anim.SetFloat("speedMult", sm);
    agent.speed *= sm;
}

// Update is called once per frame
void Update() {

    if (agent.remainingDistance < 1) {

        agent.SetDestination(goalLocations[Random.Range(0, goalLocations.Length)].transform.position);
    }
}*/