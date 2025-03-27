using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    public Button playButton;
    public Button creditsButton;
    public Button exitButton;

    void Start()
    {
        // Asignar las funciones a los botones
        playButton.onClick.AddListener(OnPlayButtonClick);
        creditsButton.onClick.AddListener(OnCreditsButtonClick);
        exitButton.onClick.AddListener(OnExitButtonClick);
    }

    // Función para iniciar la historia (escena de historia)
    void OnPlayButtonClick()
    {
        // Cambiar a la escena de historia
        SceneManager.LoadScene("StoryScene");
    }

    // Función para ir a los créditos
    void OnCreditsButtonClick()
    {
        // Cambiar a la escena de créditos
        SceneManager.LoadScene("CreditsScene");
    }

    // Función para salir del juego
    void OnExitButtonClick()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }
}
