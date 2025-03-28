using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    private static MusicManager instance;
    private AudioSource audioSource;
    public AudioClip defaultMusic;  // Música para el menú, créditos y historia
    public AudioClip gameMusic;    // Música para el juego

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // Intentar obtener el AudioSource, o crearlo si no existe
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }


    void Start()
    {
        // Si la música está vacía, poner la música por defecto (para el menú, créditos, y historia)
        if (audioSource.clip == null)
        {
            audioSource.clip = defaultMusic;
            audioSource.Play();
        }

        // Escuchar cuando cambia la escena
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // Detectar cuando cambia la escena
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Verificar la escena y reproducir la música correspondiente
        if (scene.name == "Menu" || scene.name == "CreditsScene" || scene.name == "StoryScene")
        {
            // En el menú, créditos y historia, mantén la música predeterminada
            if (audioSource.clip != defaultMusic)
            {
                audioSource.clip = defaultMusic;
                audioSource.Play();
            }
        }
        else if (scene.name == "GameScene")
        {
            // Cambiar la música cuando se entra en la escena del juego
            if (audioSource.clip != gameMusic)
            {
                audioSource.clip = gameMusic;
                audioSource.Play();
            }
        }
    }

    void OnDestroy()
    {
        // Asegurarse de dejar de escuchar eventos si el objeto es destruido
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
