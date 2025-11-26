using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitLevelTrigger : MonoBehaviour
{
    [Header("Scene To Load")]
    public string nextSceneName = "GameLevel2"; 

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene(nextSceneName);
        }
    }
}