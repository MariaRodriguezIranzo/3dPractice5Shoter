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
    // Funci�n para ir a los cr�ditos
    public void OnBackButtonClick()
    {
        // Cambiar a la escena de cr�ditos
        SceneManager.LoadScene("menu");
    }

    // Funci�n para salir del juego
    public void OnExitButtonClick()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }
}
