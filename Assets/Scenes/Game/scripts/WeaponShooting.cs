using UnityEngine;
using System.Collections;
using TMPro; // Asegúrate de importar el espacio de nombres para TextMesh Pro

public class WeaponShooting : MonoBehaviour
{
    public GameObject bulletPrefab; // Prefab de la bala
    public Transform firePoint;     // Punto desde donde disparan las balas
    public float bulletSpeed = 20f; // Velocidad de la bala
    public int maxAmmo = 10;       // Máxima munición
    private int currentAmmo;       // Munición actual

    public float reloadTime = 2f;  // Tiempo de recarga (en segundos)
    private bool isReloading = false; // Para comprobar si está recargando

    // Usamos TextMeshProUGUI en lugar de UI.Text
    public TextMeshProUGUI normalText; // El texto normal que se actualizará

    void Start()
    {
        currentAmmo = maxAmmo; // Inicializa la munición
        UpdateNormalText(); // Actualizar texto al inicio
    }

    void Update()
    {
        // Si presionas el botón de disparo y tienes munición
        if (Input.GetButtonDown("Fire1") && currentAmmo > 0 && !isReloading)
        {
            Shoot(); // Disparar
        }

        // Si presionas la tecla "R", recargar
        if (Input.GetKeyDown(KeyCode.R) && !isReloading && currentAmmo < maxAmmo)
        {
            StartCoroutine(Reload()); // Iniciar la corrutina de recarga
        }

        // Actualizar texto normal en pantalla
        UpdateNormalText();
    }

    void Shoot()
    {
        currentAmmo--; // Reducir munición
        Debug.Log("Disparo!");

        // Crear la bala
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        // Asegúrate de que la bala tenga un Rigidbody
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            // Disparar hacia adelante, usando firePoint.forward (dirección en la que está mirando el arma)
            rb.velocity = firePoint.forward * bulletSpeed; // Aplicar velocidad hacia adelante
        }
        else
        {
            Debug.LogError("La bala no tiene un Rigidbody adjunto!");
        }
    }

    // Método de recarga con un temporizador
    private IEnumerator Reload()
    {
        isReloading = true; // Marca que estamos recargando
        Debug.Log("Recargando...");

        // Espera durante el tiempo de recarga
        yield return new WaitForSeconds(reloadTime);

        // Restablece la munición
        currentAmmo = maxAmmo;
        Debug.Log("Recargado!");

        isReloading = false; // Marca que ya no estamos recargando
    }

    // Actualizar texto normal (puedes cambiar este texto para mostrar lo que quieras)
    private void UpdateNormalText()
    {
        if (normalText != null)
        {
            // Mostrar la munición en el texto, o cualquier otro mensaje que quieras
            normalText.text = currentAmmo + "/" + maxAmmo;
        }
    }
}
