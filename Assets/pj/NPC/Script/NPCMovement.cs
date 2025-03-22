using System;
using UnityEngine;
using UnityEngine.AI;

public class NPCMovement : MonoBehaviour
{
    public Transform target; // Objeto destino
    private NavMeshAgent agent;
    private Animator animator; // Cambiado a Animator

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>(); // Usar Animator
        agent.SetDestination(target.position);
    }

    void Update()
    {
        float speed = agent.velocity.magnitude; // Obtener velocidad del agente
        animator.SetFloat("Speed", speed); // Ajustar el parámetro Speed en el Animator
    }
}
