using UnityEngine;
using System.Collections;
using TMPro; // Aseg�rate de importar el espacio de nombres para TextMesh Pro

public class WeaponShooting : MonoBehaviour
{
    public GameObject bulletPrefab; // Prefab de la bala
    public Transform firePoint;     // Punto desde donde disparan las balas
    public float bulletSpeed = 20f; // Velocidad de la bala
    public int maxAmmo = 10;       // M�xima munici�n
    private int currentAmmo;       // Munici�n actual

    public float reloadTime = 2f;  // Tiempo de recarga (en segundos)
    private bool isReloading = false; // Para comprobar si est� recargando

    // Usamos TextMeshProUGUI en lugar de UI.Text
    public TextMeshProUGUI normalText; // El texto normal que se actualizar�

    void Start()
    {
        currentAmmo = maxAmmo; // Inicializa la munici�n
        UpdateNormalText(); // Actualizar texto al inicio
    }

    void Update()
    {
        // Si presionas el bot�n de disparo y tienes munici�n
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
        currentAmmo--; // Reducir munici�n
        Debug.Log("Disparo!");

        // Crear la bala
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        // Aseg�rate de que la bala tenga un Rigidbody
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            // Disparar hacia adelante, usando firePoint.forward (direcci�n en la que est� mirando el arma)
            rb.velocity = firePoint.forward * bulletSpeed; // Aplicar velocidad hacia adelante
        }
        else
        {
            Debug.LogError("La bala no tiene un Rigidbody adjunto!");
        }
    }

    // M�todo de recarga con un temporizador
    private IEnumerator Reload()
    {
        isReloading = true; // Marca que estamos recargando
        Debug.Log("Recargando...");

        // Espera durante el tiempo de recarga
        yield return new WaitForSeconds(reloadTime);

        // Restablece la munici�n
        currentAmmo = maxAmmo;
        Debug.Log("Recargado!");

        isReloading = false; // Marca que ya no estamos recargando
    }

    // Actualizar texto normal (puedes cambiar este texto para mostrar lo que quieras)
    private void UpdateNormalText()
    {
        if (normalText != null)
        {
            // Mostrar la munici�n en el texto, o cualquier otro mensaje que quieras
            normalText.text = currentAmmo + "/" + maxAmmo;
        }
    }
}
