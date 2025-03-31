using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    public GameObject ak74Suelo;
    void OnTriggerEnter(Collider other)
    {
        // Verificar si el jugador ha tocado el arma
        if (other.CompareTag("Player"))
        {
            WeaponInventory inventory = other.GetComponent<WeaponInventory>();

            if (inventory != null)
            {
                inventory.PickupWeapon();

                // Ocultar el arma del suelo en lugar de destruirla
                ak74Suelo.SetActive(false);

            }
        }
    }
}
