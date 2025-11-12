using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("GameLevel");
    }

    public void ShowInstructions()
    {
        SceneManager.LoadScene("Instructions");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}