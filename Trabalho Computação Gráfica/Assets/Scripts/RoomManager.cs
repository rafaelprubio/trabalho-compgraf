using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public GameObject door; // porta fechada
    public GameObject[] enemies; // infectados
    public AudioClip doorOpenSound;

    void Update()
    {
        if (AllEnemiesCured())
        {
            UnlockDoor();
        }
    }

    bool AllEnemiesCured()
    {
        foreach (GameObject enemyObj in enemies)
        {
            if (enemyObj != null) // nao foi destruido
                return false;
        }
        return true;
    }

    void UnlockDoor()
    {
        if (door != null && door.activeSelf)
        {
            door.SetActive(false);
            AudioSource.PlayClipAtPoint(doorOpenSound, door.transform.position);
        }
    }
}