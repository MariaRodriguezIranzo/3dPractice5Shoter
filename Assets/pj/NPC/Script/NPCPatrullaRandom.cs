using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCPatrullaRandom : MonoBehaviour
{
    public Transform[] waypoints;
    private NavMeshAgent agent;
    private Animator animator;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        StartCoroutine(PatrolRandomly());
    }

    IEnumerator PatrolRandomly()
    {
        while (true)
        {
            Transform nextWaypoint = GetRandomAccessibleWaypoint();
            if (nextWaypoint != null)
            {
                agent.SetDestination(nextWaypoint.position);
                animator.SetFloat("Speed", agent.speed);
                yield return new WaitUntil(() => agent.remainingDistance < 0.5f);
            }
        }
    }

    Transform GetRandomAccessibleWaypoint()
    {
        List<Transform> availableWaypoints = new List<Transform>();
        foreach (Transform waypoint in waypoints)
        {
            NavMeshPath path = new NavMeshPath();
            if (agent.CalculatePath(waypoint.position, path) && path.status == NavMeshPathStatus.PathComplete)
            {
                availableWaypoints.Add(waypoint);
            }
        }
        return availableWaypoints.Count > 0 ? availableWaypoints[Random.Range(0, availableWaypoints.Count)] : null;
    }
}
