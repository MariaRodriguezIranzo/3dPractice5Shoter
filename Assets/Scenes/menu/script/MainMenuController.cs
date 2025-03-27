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

    // Funci�n para iniciar la historia (escena de historia)
    void OnPlayButtonClick()
    {
        // Cambiar a la escena de historia
        SceneManager.LoadScene("StoryScene");
    }

    // Funci�n para ir a los cr�ditos
    void OnCreditsButtonClick()
    {
        // Cambiar a la escena de cr�ditos
        SceneManager.LoadScene("CreditsScene");
    }

    // Funci�n para salir del juego
    void OnExitButtonClick()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }
}
