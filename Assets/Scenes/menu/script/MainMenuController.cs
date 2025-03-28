using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    // Funci�n para iniciar la historia (escena de historia)
    public void OnPlayButtonClick()
    {
        // Cambiar a la escena de historia
        SceneManager.LoadScene("StoryScene");
    }

    // Funci�n para ir a los cr�ditos
   public void  OnCreditsButtonClick()
    {
        // Cambiar a la escena de cr�ditos
        SceneManager.LoadScene("CreditsScene");
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
