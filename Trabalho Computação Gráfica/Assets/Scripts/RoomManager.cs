using UnityEngine;

public class RoomManager : MonoBehaviour
{
    [Header("Room Settings")]
    public GameObject door;
    public GameObject exitTrigger;
    public AudioClip doorOpenSound;
    public GameObject questArrow;
    
    private bool levelComplete = false;

    void Start()
    {
        if (exitTrigger != null) exitTrigger.SetActive(false);
        if (questArrow != null) questArrow.SetActive(false);
    }

    void Update()
    {
        if (!levelComplete)
        {
            if (AllEnemiesCleared())
            {
                CompleteRoom();
            }
        }
    }

    bool AllEnemiesCleared()
    {
        InfectedEnemy[] allEnemies = FindObjectsOfType<InfectedEnemy>();
        if (allEnemies.Length == 0) return true;
        foreach (InfectedEnemy enemy in allEnemies)
        {
            if (enemy.isInfected)
            {
                return false;
            }
        }
        return true;
    }

    void CompleteRoom()
    {
        levelComplete = true;
        if (GameManager.Instance != null)
            GameManager.Instance.UpdateObjective("Setor limpo! Va para a porta.");
        if (door != null)
        {
            door.SetActive(false);
            if(doorOpenSound) AudioSource.PlayClipAtPoint(doorOpenSound, door.transform.position);
        }
        if (exitTrigger != null)
        {
            exitTrigger.SetActive(true);
        }
        if (questArrow != null) questArrow.SetActive(true);
    }
}