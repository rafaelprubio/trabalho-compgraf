using UnityEngine;
using TMPro;

public class Room2Survival : MonoBehaviour
{
    [Header("Level Settings")]
    public float survivalTime = 60f;
    public EnemySpawner spawner;
    public GameObject exitTrigger;
    public GameObject door;
    public GameObject questArrow;
    
    [Header("Audio")]
    public AudioClip winSound;
    public AudioClip alarmSound;

    private float timeRemaining;
    private bool levelComplete = false;
    private AudioSource audioSource;

    void Start()
    {
        timeRemaining = survivalTime;
        audioSource = GetComponent<AudioSource>();
        if (exitTrigger != null) exitTrigger.SetActive(false);
        if (questArrow != null) questArrow.SetActive(false);
        
        if (GameManager.Instance != null)
            GameManager.Instance.UpdateObjective("Proteja o Civil! Tempo: " + Mathf.CeilToInt(timeRemaining));
            
        if (alarmSound != null && audioSource != null)
        {
            audioSource.clip = alarmSound;
            audioSource.loop = true;
            audioSource.Play();
        }
    }

    void Update()
    {
        if (levelComplete) return;
        timeRemaining -= Time.deltaTime;
        if (GameManager.Instance != null)
        {
            GameManager.Instance.UpdateObjective("Proteja o Civil! Tempo: " + Mathf.CeilToInt(timeRemaining));
        }
        if (timeRemaining <= 0)
        {
            LevelComplete();
        }
    }

    void LevelComplete()
    {
        levelComplete = true;
        if (audioSource != null) audioSource.Stop();
        if (winSound != null) AudioSource.PlayClipAtPoint(winSound, transform.position);
        if (spawner != null) spawner.StopSpawning();
        KillAllRemainingEnemies();
        if (door != null) door.SetActive(false);
        if (exitTrigger != null) exitTrigger.SetActive(true);
        if (questArrow != null) questArrow.SetActive(true);

        if (GameManager.Instance != null)
            GameManager.Instance.UpdateObjective("Setor seguro. Va para o ponto de extracao!");
    }

    void KillAllRemainingEnemies()
    {
        InfectedEnemy[] enemies = FindObjectsOfType<InfectedEnemy>();
        foreach (InfectedEnemy enemy in enemies)
        {
            enemy.GetCured();
        }
    }
}