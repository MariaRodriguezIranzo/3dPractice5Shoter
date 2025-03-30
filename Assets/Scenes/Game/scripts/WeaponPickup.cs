using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    public GameObject weaponPrefab; // Prefab del arma a recoger

    void OnTriggerEnter(Collider other)
    {
        // Verificar si el jugador ha tocado el arma
        if (other.CompareTag("Player"))
        {
            WeaponInventory inventory = other.GetComponent<WeaponInventory>();

            if (inventory != null)
            {
                // Llamar al método para recoger el arma
                inventory.PickupWeapon(weaponPrefab);

                // Destruir el objeto del arma en el suelo
                Destroy(gameObject); // Solo destruir el objeto del suelo
            }
        }
    }
}
