using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCPatrol : MonoBehaviour
{
    public Transform[] waypoints;
    private NavMeshAgent agent;
    private int currentWaypoint = 0;
    private Animator animator;
    public float waitTime = 2f; // Tiempo de espera en cada waypoint

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        StartCoroutine(Patrol());
    }

    IEnumerator Patrol()
    {
        while (true)
        {
            if (WaypointAccesible(waypoints[currentWaypoint]))
            {
                agent.SetDestination(waypoints[currentWaypoint].position);
                animator.SetFloat("Speed", agent.speed);
                yield return new WaitUntil(() => agent.remainingDistance < 0.5f);
                yield return new WaitForSeconds(waitTime);
                currentWaypoint = (currentWaypoint + 1) % waypoints.Length;
            }
            else
            {
                currentWaypoint = (currentWaypoint + 1) % waypoints.Length; // Saltar el waypoint bloqueado
            }
        }
    }

    bool WaypointAccesible(Transform waypoint)
    {
        NavMeshPath path = new NavMeshPath();
        return agent.CalculatePath(waypoint.position, path) && path.status == NavMeshPathStatus.PathComplete;
    }
}
