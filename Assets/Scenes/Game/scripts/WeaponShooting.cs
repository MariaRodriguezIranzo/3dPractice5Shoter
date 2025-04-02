using System.Collections;
using UnityEngine;
using TMPro;

public class WeaponShooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 20f;
    public int maxAmmo = 10;
    private int currentAmmo;
    public float reloadTime = 12f;
    private bool isReloading = false;
    public TextMeshProUGUI normalText;
    public AudioSource audioSource;
    public AudioClip shootSound;
    public AudioClip reloadSound;

    void Start()
    {
        currentAmmo = maxAmmo;
        UpdateAmmoText();
    }

    void Update()
    {
        // Verifica si el jugador hace clic para disparar
        if (Input.GetButtonDown("Fire1") && currentAmmo > 0 && !isReloading)
        {
            Shoot();
        }

        // Verifica si el jugador presiona "R" para recargar
        if (Input.GetKeyDown(KeyCode.R) && !isReloading && currentAmmo < maxAmmo)
        {
            StartCoroutine(Reload());
        }

        // Actualiza el texto de la munición
        UpdateAmmoText();
    }

    void Shoot()
    {
        // Disminuye la munición y realiza el disparo
        currentAmmo--;

        // Reproduce el sonido de disparo si está configurado
        if (audioSource && shootSound)
        {
            audioSource.PlayOneShot(shootSound);
        }

        // Instancia la bala y le asigna velocidad
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = firePoint.up * bulletSpeed;
        }
    }

    private IEnumerator Reload()
    {
        // Inicia la recarga
        isReloading = true;

        // Reproduce el sonido de recarga si está configurado y lo reproduce en bucle
        if (audioSource && reloadSound)
        {
            audioSource.clip = reloadSound;      // Asignamos el sonido de recarga
            audioSource.loop = true;             // Activamos el loop
            audioSource.Play();                  // Reproducimos el sonido
        }

        // Espera el tiempo de recarga
        yield return new WaitForSeconds(reloadTime);

        // Detiene el sonido de recarga una vez terminado
        if (audioSource)
        {
            audioSource.loop = false;            // Desactivamos el loop
            audioSource.Stop();                  // Detenemos el sonido
        }

        // Restaura la munición y termina la recarga
        currentAmmo = maxAmmo;
        isReloading = false;
    }

    private void UpdateAmmoText()
    {
        // Actualiza el texto que muestra la munición actual
        if (normalText != null)
        {
            normalText.text = currentAmmo + "/" + maxAmmo;
        }
    }
}
