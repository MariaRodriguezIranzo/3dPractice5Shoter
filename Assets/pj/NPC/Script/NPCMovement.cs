using System;
using UnityEngine;
using UnityEngine.AI;

public class NPCMovement : MonoBehaviour
{
    public Transform target;
    private NavMeshAgent agent;
    private Animator animator;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        agent.SetDestination(target.position);
    }

    void Update()
    {

        if (agent.remainingDistance > agent.stoppingDistance)
        {
            animator.SetFloat("Speed", 3f);

        }
        // Add stop animation if path is complete
        if (agent.pathStatus == NavMeshPathStatus.PathComplete &&
            agent.remainingDistance <= agent.stoppingDistance)
        {
            animator.SetFloat("Speed", 0f);

        }
    }
}