using UnityEngine;
using UnityEngine.SceneManagement; // Necesario para cambiar de escena
using System.Collections; // Necesario para corutinas

public class NPCManager : MonoBehaviour
{
    public GameObject bluefireDog;  // El perro con el tag Bluefire
    public string winSceneName = "WIN"; // Nombre de la escena de victoria

    void Start()
    {
        if (bluefireDog != null)
        {
            bluefireDog.SetActive(false); // Desactivamos el perro al inicio
            Debug.Log("Perro Bluefire desactivado al inicio.");
        }
        else
        {
            Debug.LogError("El perro Bluefire no está asignado en el Inspector.");
        }
    }

    void Update()
    {
        // Comprobar si todos los NPC están derrotados
        GameObject[] npcEnemies = GameObject.FindGameObjectsWithTag("NPC");
        bool allEnemiesDefeated = npcEnemies.Length == 0;

        if (allEnemiesDefeated)
        {
            // Si todos los enemigos están derrotados y el perro aún no está activo, activarlo
            if (bluefireDog != null && !bluefireDog.activeSelf)
            {
                bluefireDog.SetActive(true);
                Debug.Log("El perro Bluefire ha aparecido.");

                // Llamamos a la corutina para esperar 30 segundos y cargar la escena de victoria
                StartCoroutine(ShowDogAndLoadWinScene());
            }
        }
    }

    IEnumerator ShowDogAndLoadWinScene()
    {
        // Esperar 30 segundos mientras el perro está visible
        yield return new WaitForSeconds(30f);

        // Cargar la escena de victoria
        Debug.Log("Cargando la escena de victoria...");
        SceneManager.LoadScene(winSceneName); // Carga la escena de victoria
    }
}
