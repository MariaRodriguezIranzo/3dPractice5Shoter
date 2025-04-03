using UnityEngine;
using UnityEngine.SceneManagement;

public class NPCManager : MonoBehaviour
{
    public GameObject[] npcEnemies; // Los NPCs en la escena
    public GameObject bluefireDog;  // El perro con el tag Bluefire
    public Transform winArea;       // Zona donde el jugador debe llegar
    public string winSceneName = "WinScene"; // Nombre de la escena de victoria

    private bool allEnemiesDefeated = false;
    private bool playerInWinArea = false;

    void Start()
    {
        // Inicialmente desactivamos el perro
        if (bluefireDog != null)
        {
            bluefireDog.SetActive(false);
        }
    }

    void Update()
    {
        // Verificar si todos los NPCs han sido derrotados
        if (!allEnemiesDefeated)
        {
            allEnemiesDefeated = CheckEnemiesDefeated();
        }

        // Si todos los NPCs han sido derrotados y el jugador está en el área de victoria, mostramos el perro y cambiamos a la escena de victoria
        if (allEnemiesDefeated && playerInWinArea)
        {
            ShowDogAndLoadWinScene();
        }
    }

    // Verificar si todos los NPCs han sido derrotados
    bool CheckEnemiesDefeated()
    {
        foreach (var npc in npcEnemies)
        {
            if (npc != null) // Si algún NPC sigue vivo, no se considera derrotado
            {
                return false;
            }
        }
        return true; // Todos los NPCs han sido derrotados
    }

    // Mostrar el perro y cargar la escena de victoria
    void ShowDogAndLoadWinScene()
    {
        if (bluefireDog != null && !bluefireDog.activeSelf)
        {
            bluefireDog.SetActive(true); // Mostrar el perro Bluefire
        }

        // Cargar la escena de victoria
        SceneManager.LoadScene(winSceneName);
    }

    // Detectar si el jugador entra en el área de victoria
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Si el jugador entra en el área
        {
            playerInWinArea = true; // El jugador ha llegado al área de victoria
        }
    }

    // Detectar si el jugador sale del área de victoria
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInWinArea = false; // El jugador ha salido del área de victoria
        }
    }
}
