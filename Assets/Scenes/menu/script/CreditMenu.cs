using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CreditMenu : MonoBehaviour
{
    public Button backButton;
    public Button exitButton;

     void  Start()
    {
        // Asignar las funciones a los botones

        backButton.onClick.AddListener(OnBackButtonClick);
        exitButton.onClick.AddListener(OnExitButtonClick);
    }
    // Función para ir a los créditos
    public void OnBackButtonClick()
    {
        // Cambiar a la escena de créditos
        SceneManager.LoadScene("menu");
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
