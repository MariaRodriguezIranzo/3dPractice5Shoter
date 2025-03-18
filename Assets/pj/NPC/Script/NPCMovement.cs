using UnityEngine;
using UnityEngine.AI;

public class NPCMovement : MonoBehaviour
{
    public Transform target; // Objeto destino
    private NavMeshAgent agent;
    private Animator anim;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>(); // Obtener el Animator

        if (target != null)
        {
            agent.SetDestination(target.position);
        }
    }

    void Update()
    {
        if (target != null && agent.enabled)
        {
            agent.SetDestination(target.position);

            // Actualizar el parámetro "Speed" en el Animator
            anim.SetFloat("Speed", agent.velocity.magnitude);
        }
    }
}
