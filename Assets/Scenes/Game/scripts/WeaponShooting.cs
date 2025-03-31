using UnityEngine;

public class WeaponShooting : MonoBehaviour
{
    public Camera playerCamera;
    public Transform firePoint;
    public float fireRate = 0.2f;
    public int maxAmmo = 10;
    private int currentAmmo;
    private float nextFireTime = 0f;

    void Start()
    {
        currentAmmo = maxAmmo;
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1") && Time.time >= nextFireTime && currentAmmo > 0)
        {
            nextFireTime = Time.time + fireRate;
            Shoot();
        }
    }

    void Shoot()
    {
        currentAmmo--;
        Debug.Log("Disparo!");
        RaycastHit hit;
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, 50f))
        {
            Debug.Log("Impacto en: " + hit.transform.name);
        }
    }

    public void Reload()
    {
        currentAmmo = maxAmmo;
        Debug.Log("Recargado!");
    }
}
