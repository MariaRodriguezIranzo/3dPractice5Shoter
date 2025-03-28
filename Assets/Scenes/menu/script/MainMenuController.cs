using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    // Función para iniciar la historia (escena de historia)
    public void OnPlayButtonClick()
    {
        // Cambiar a la escena de historia
        SceneManager.LoadScene("StoryScene");
    }

    // Función para ir a los créditos
   public void  OnCreditsButtonClick()
    {
        // Cambiar a la escena de créditos
        SceneManager.LoadScene("CreditsScene");
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
