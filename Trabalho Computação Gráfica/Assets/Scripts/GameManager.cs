using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Score & Objectives")]
    public static int globalScore = 0; 

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI objectiveText;
    public string startMessage = "Purifique todos os monstros infectados!"; 

    [Header("UI Panels")]
    public GameObject gameOverPanel;
    public GameObject victoryPanel;
    public TextMeshProUGUI finalScoreText;
    [Header("Audio")]
    public AudioClip normalMusic;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(Instance.gameObject);
        }
        Instance = this;
    }

    void Start()
    {
        if (SceneManager.GetActiveScene().name == "GameLevel1")
        {
            globalScore = 0;
            if (BackgroundMusic.Instance != null && normalMusic != null)
            {
                BackgroundMusic.Instance.ChangeMusic(normalMusic);
            } 
        }

        UpdateScoreUI();
        UpdateObjective(startMessage); 
        Time.timeScale = 1f;
    }

    public void AddScore(int points)
    {
        globalScore += points;
        UpdateScoreUI();
    }

    void UpdateScoreUI()
    {
        if (scoreText != null)
            scoreText.text = "Pontos: " + globalScore;
    }

    public void UpdateObjective(string newText)
    {
        if (objectiveText != null)
            objectiveText.text = newText;
    }

    public void GameOver()
    {
        if(gameOverPanel) gameOverPanel.SetActive(true);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void Victory()
    {
        if(victoryPanel) victoryPanel.SetActive(true);
        if(finalScoreText) finalScoreText.text = "Pontuacao Final: " + globalScore;
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
    public void RestartEntireGame()
    {
        globalScore = 0;
        Time.timeScale = 1f;
        SceneManager.LoadScene("GameLevel1");
    }
}