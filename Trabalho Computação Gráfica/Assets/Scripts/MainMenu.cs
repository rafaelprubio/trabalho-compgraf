using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [Header("Audio")]
    public AudioClip menuMusic;

    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        if (BackgroundMusic.Instance != null && menuMusic != null)
        {
            BackgroundMusic.Instance.ChangeMusic(menuMusic);
        }
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("GameLevel1"); 
    }

    public void ShowInstructions()
    {
        SceneManager.LoadScene("Instructions");
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();

        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}