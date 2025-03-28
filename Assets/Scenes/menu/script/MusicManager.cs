using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    private static MusicManager instance;
    private AudioSource audioSource;
    public AudioClip defaultMusic;  // M�sica para el men�, cr�ditos y historia
    public AudioClip gameMusic;    // M�sica para el juego

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
        // Si la m�sica est� vac�a, poner la m�sica por defecto (para el men�, cr�ditos, y historia)
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
        // Verificar la escena y reproducir la m�sica correspondiente
        if (scene.name == "Menu" || scene.name == "CreditsScene" || scene.name == "StoryScene")
        {
            // En el men�, cr�ditos y historia, mant�n la m�sica predeterminada
            if (audioSource.clip != defaultMusic)
            {
                audioSource.clip = defaultMusic;
                audioSource.Play();
            }
        }
        else if (scene.name == "GameScene")
        {
            // Cambiar la m�sica cuando se entra en la escena del juego
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
