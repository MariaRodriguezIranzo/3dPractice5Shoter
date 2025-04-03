using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WaypointBlocker : MonoBehaviour
{
    public float activationDelay = 10f;
    private NavMeshObstacle obstacle;

    void Start()
    {
        obstacle = GetComponent<NavMeshObstacle>();
        obstacle.carving = false;
        StartCoroutine(ActivateObstacle());
    }

    IEnumerator ActivateObstacle()
    {
        yield return new WaitForSeconds(activationDelay);
        obstacle.carving = true;
    }
}
