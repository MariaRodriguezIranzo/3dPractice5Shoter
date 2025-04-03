using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStateMachine : MonoBehaviour
{
    public enum State
    {
        Patrol,
        MeleeAttack,
        RangedAttack
    }

    public State currentState;
    public Transform[] waypoints;
    private NavMeshAgent agent;
    private Animator animator;
    private Transform player;

    [Header("Rangos")]
    public float meleeAttackRange = 2f;
    public float rangedAttackRange = 10f;
    public float detectionRange = 20f;

    [Header("Ataque a distancia")]
    public GameObject projectilePrefab; // Prefab de la bala
    public Transform firePoint; // Punto desde donde se dispara
    public float projectileSpeed = 15f;
    public float attackCooldown = 1.5f; // Tiempo entre disparos

    private int currentWaypoint = 0;
    private bool canAttack = true;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        player = GameObject.FindWithTag("Player").transform;
        ChangeState(State.Patrol);
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Cambiar de estado según la distancia al jugador
        if (distanceToPlayer <= meleeAttackRange)
        {
            ChangeState(State.MeleeAttack);
        }
        else if (distanceToPlayer <= rangedAttackRange)
        {
            ChangeState(State.RangedAttack);
        }
        else
        {
            ChangeState(State.Patrol);
        }

        // Ejecutar la lógica del estado actual
        switch (currentState)
        {
            case State.Patrol:
                Patrol();
                break;
            case State.MeleeAttack:
                MeleeAttack();
                break;
            case State.RangedAttack:
                RangedAttack();
                break;
        }
    }

    // Cambiar el estado
    void ChangeState(State newState)
    {
        if (currentState == newState) return; // Evitar cambios innecesarios
        currentState = newState;

        // Mantener la animación de movimiento SIEMPRE
        animator.SetFloat("Speed", 3f);

        switch (newState)
        {
            case State.Patrol:
                SetNextWaypoint();
                break;
            case State.MeleeAttack:
                agent.SetDestination(player.position);
                break;
            case State.RangedAttack:
                agent.SetDestination(player.position);
                break;
        }
    }

    // Patrullaje entre waypoints
    void Patrol()
    {
        if (agent.remainingDistance < 0.5f)
        {
            currentWaypoint = (currentWaypoint + 1) % waypoints.Length;
            agent.SetDestination(waypoints[currentWaypoint].position);
        }
    }

    // Ataque cuerpo a cuerpo
    void MeleeAttack()
    {
        agent.SetDestination(player.position);

        if (canAttack && Vector3.Distance(transform.position, player.position) <= meleeAttackRange)
        {
            Debug.Log("Ataque cuerpo a cuerpo al jugador");
            StartCoroutine(AttackCooldown());
        }
    }

    // Ataque a distancia
    void RangedAttack()
    {
        agent.SetDestination(player.position); // Seguir al jugador mientras dispara

        if (canAttack && Vector3.Distance(transform.position, player.position) <= rangedAttackRange)
        {
            ShootProjectile();
            StartCoroutine(AttackCooldown());
        }
    }
    // Disparar un proyectil
    void ShootProjectile()
    {
        if (firePoint && projectilePrefab)
        {
            // Instanciar el proyectil en el firePoint
            GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
            BulletNPC bulletScript = projectile.GetComponent<BulletNPC>();

            if (bulletScript != null)
            {
                // Eliminar la línea que intenta asignar el shooter
                // Ya no es necesario asignar el shooter, así que lo dejamos de lado.

                bulletScript.bulletSpeed = projectileSpeed; // Ajusta la velocidad del proyectil
            }

            Debug.Log("NPC disparó un proyectil");
        }
    }

    // Esperar entre ataques
    IEnumerator AttackCooldown()
    {
        canAttack = false;
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }

    // Establecer el siguiente waypoint para patrullaje
    void SetNextWaypoint()
    {
        agent.SetDestination(waypoints[currentWaypoint].position);
    }
}
