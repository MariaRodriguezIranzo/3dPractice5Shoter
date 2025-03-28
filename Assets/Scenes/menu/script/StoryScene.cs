using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StoryScene : MonoBehaviour
{
    public Button playButton;
    public Button exitButton;

    void Start()
    {
        // Asignar las funciones a los botones

        playButton.onClick.AddListener(OnPlayButtonClick);
        exitButton.onClick.AddListener(OnExitButtonClick);
    }

    // Función para iniciar la historia (escena de historia)
    public void OnPlayButtonClick()
    {
        // Cambiar a la escena de historia
        SceneManager.LoadScene("Game");
    }

    // Función para salir del juego
    public void OnExitButtonClick()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }
}
