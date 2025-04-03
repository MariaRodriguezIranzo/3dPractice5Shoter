using UnityEngine;

public class BulletNPC : MonoBehaviour
{
    public float bulletSpeed = 10f;  // Velocidad de la bala
    public float lifetime = 2f;      // Tiempo de vida de la bala
    public int damage = 1;           // Da�o de la bala
    private Rigidbody rb;
    public AudioClip shootingSound;  // Sonido del disparo
    private AudioSource audioSource;

    private Transform playerTransform; // Necesitamos la posici�n del jugador para disparar hacia �l

    void Start()
    {
        rb = GetComponent<Rigidbody>();  // Aseg�rate de que tienes un Rigidbody asignado

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

        // Verifica que el jugador est� presente
        if (playerTransform != null)
        {
            Vector3 directionToPlayer = (playerTransform.position - transform.position).normalized;  // Direcci�n hacia el jugador
            rb.velocity = directionToPlayer * bulletSpeed;  // Mueve la bala hacia el jugador
        }
        else
        {
            Debug.LogWarning("No se ha encontrado al jugador para disparar.");
        }

        Destroy(gameObject, lifetime);  // Destruir la bala despu�s de un tiempo
    }

    void OnTriggerEnter(Collider other)
    {
        // Aseg�rate de que las colisiones est�n siendo detectadas correctamente
        Debug.Log("Colisi�n detectada con: " + other.name);

        // Verifica si la bala colisiona con el jugador
        if (other.CompareTag("Player"))
        {
            ControllerPlayer player = other.GetComponent<ControllerPlayer>(); // Obtener el componente del jugador

            if (player != null)
            {
                Debug.Log("Bala del NPC impact� al jugador.");
                StartCoroutine(player.RecibirDa�o());  // Llamar al m�todo de da�o del jugador
                Destroy(gameObject);  // Destruir la bala al impactar con el jugador
                return;
            }
        }
    }
}
