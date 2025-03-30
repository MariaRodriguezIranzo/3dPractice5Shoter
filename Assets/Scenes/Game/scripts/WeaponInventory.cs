using UnityEngine;

public class WeaponInventory : MonoBehaviour
{
    public Transform weaponHolder; // Lugar donde se colocan las armas en la mano
    public GameObject defaultWeaponPrefab;  // Prefab del arma inicial

    private GameObject defaultWeapon;  // Arma inicial en la mano
    private GameObject secondaryWeapon; // Arma recogida
    private GameObject activeWeapon;  // Arma actualmente equipada

    public GameObject gun1UI; // Icono del arma 1 en el inventario
    public GameObject gun2UI; // Icono del arma 2 en el inventario

    private bool hasSecondaryWeapon = false; // Si el jugador ha recogido un arma
    private int currentWeaponIndex = 1; // 1 = Pistola, 2 = Arma recogida

    void Start()
    {
        // Instanciar el arma inicial en la mano
        defaultWeapon = Instantiate(defaultWeaponPrefab, weaponHolder);
        SetWeaponTransform(defaultWeapon);
        EquipWeapon(defaultWeapon); // Equipar el arma predeterminada
    }

    void Update()
    {
        // Cambiar a la pistola
        if (Input.GetKeyDown(KeyCode.Alpha1) && currentWeaponIndex != 1)
        {
            currentWeaponIndex = 1;
            EquipWeapon(defaultWeapon);
        }

        // Cambiar al arma secundaria
        if (Input.GetKeyDown(KeyCode.Alpha2) && hasSecondaryWeapon && currentWeaponIndex != 2)
        {
            currentWeaponIndex = 2;
            EquipWeapon(secondaryWeapon);
        }
    }

    // Método para equipar el arma
    void EquipWeapon(GameObject weapon)
    {
        if (weapon == null) return;

        // Desactivar el arma anterior
        if (activeWeapon != null) activeWeapon.SetActive(false);

        // Establecer la nueva arma activa
        activeWeapon = weapon;
        activeWeapon.SetActive(true); // Activar el arma actual

        // Actualizar el HUD del inventario
        gun1UI.SetActive(currentWeaponIndex == 1);
        gun2UI.SetActive(currentWeaponIndex == 2);
    }

    // Método para recoger un arma
    public void PickupWeapon(GameObject newWeaponPrefab)
    {
        if (hasSecondaryWeapon) return; // No permitir recoger más de un arma secundaria

        // Instanciar el arma recogida en la mano y mantener la referencia
        secondaryWeapon = Instantiate(newWeaponPrefab, weaponHolder);
        SetWeaponTransform(secondaryWeapon);
        secondaryWeapon.SetActive(false); // Mantenerla oculta hasta que se equipe

        hasSecondaryWeapon = true; // Ahora el jugador tiene un arma secundaria
        gun2UI.SetActive(true); // Mostrar el ícono de arma 2 en el inventario

        Debug.Log("Arma recogida: " + newWeaponPrefab.name);
    }

    // Ajustar la posición y rotación del arma
    private void SetWeaponTransform(GameObject weapon)
    {
        weapon.transform.localPosition = new Vector3(0.0235f, 0.1705f, 0.0141f);
        weapon.transform.localRotation = Quaternion.Euler(180f, -90f, -280f);
    }
}
