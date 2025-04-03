using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections; // Necesario para corutinas

public class NPCManager : MonoBehaviour
{
    public GameObject bluefireDog;  // El perro con el tag Bluefire
    public Transform winArea;       // Zona donde el jugador debe llegar
    public string winSceneName = "WinScene"; // Nombre de la escena de victoria

    private bool allEnemiesDefeated = false;
    private bool playerInWinArea = false;

    void Start()
    {
        if (bluefireDog != null)
        {
            bluefireDog.SetActive(false);
            Debug.Log("Perro Bluefire desactivado al inicio.");
        }
        else
        {
            Debug.LogError("El perro Bluefire no está asignado en el Inspector.");
        }
    }

    void Update()
    {
        if (!allEnemiesDefeated)
        {
            allEnemiesDefeated = CheckEnemiesDefeated();
        }

        if (allEnemiesDefeated && playerInWinArea)
        {
            StartCoroutine(ShowDogAndLoadWinScene());
        }
    }

    bool CheckEnemiesDefeated()
    {
        GameObject[] npcEnemies = GameObject.FindGameObjectsWithTag("NPC");
        bool allDefeated = npcEnemies.Length == 0;

        Debug.Log("NPCs restantes: " + npcEnemies.Length);
        return allDefeated;
    }

    IEnumerator ShowDogAndLoadWinScene()
    {
        if (bluefireDog != null && !bluefireDog.activeSelf)
        {
            bluefireDog.SetActive(true);
            Debug.Log("El perro Bluefire ha aparecido.");
            yield return new WaitForSeconds(2f);
        }
        else
        {
            Debug.LogError("El perro Bluefire no se activó correctamente.");
        }

        Debug.Log("Cargando la escena de victoria...");
        SceneManager.LoadScene(winSceneName);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInWinArea = true;
            Debug.Log("Jugador ha llegado al área de victoria.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInWinArea = false;
            Debug.Log("Jugador ha salido del área de victoria.");
        }
    }
}
