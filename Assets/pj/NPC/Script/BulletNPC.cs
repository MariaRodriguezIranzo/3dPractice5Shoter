using UnityEngine;

public class BulletNPC : MonoBehaviour
{
    public float bulletSpeed = 10f;  // Velocidad de la bala
    public float lifetime = 2f;      // Tiempo de vida de la bala
    public int damage = 1;           // Daño de la bala
    private Rigidbody rb;
    public AudioClip shootingSound;  // Sonido del disparo
    private AudioSource audioSource;

    private Transform playerTransform; // Necesitamos la posición del jugador para disparar hacia él

    void Start()
    {
        rb = GetComponent<Rigidbody>();  // Asegúrate de que tienes un Rigidbody asignado

        if (rb == null)
        {
            Debug.LogError("Falta Rigidbody en la bala!");
            return;
        }

        audioSource = GetComponent<AudioSource>();  // Obtener el AudioSource

        if (audioSource != null && shootingSound != null)
        {
            audioSource.PlayOneShot(shootingSound);  // Reproduce el sonido del disparo
        }

        // Encuentra al jugador en la escena
        playerTransform = GameObject.FindWithTag("Player").transform;

        // Verifica que el jugador esté presente
        if (playerTransform != null)
        {
            Vector3 directionToPlayer = (playerTransform.position - transform.position).normalized;  // Dirección hacia el jugador
            rb.velocity = directionToPlayer * bulletSpeed;  // Mueve la bala hacia el jugador
        }
        else
        {
            Debug.LogWarning("No se ha encontrado al jugador para disparar.");
        }

        Destroy(gameObject, lifetime);  // Destruir la bala después de un tiempo
    }

    void OnTriggerEnter(Collider other)
    {
        // Asegúrate de que las colisiones están siendo detectadas correctamente
        Debug.Log("Colisión detectada con: " + other.name);

        // Verifica si la bala colisiona con el jugador
        if (other.CompareTag("Player"))
        {
            ControllerPlayer player = other.GetComponent<ControllerPlayer>(); // Obtener el componente del jugador

            if (player != null)
            {
                Debug.Log("Bala del NPC impactó al jugador.");
                StartCoroutine(player.RecibirDaño());  // Llamar al método de daño del jugador
                Destroy(gameObject);  // Destruir la bala al impactar con el jugador
                return;
            }
        }
    }
}
