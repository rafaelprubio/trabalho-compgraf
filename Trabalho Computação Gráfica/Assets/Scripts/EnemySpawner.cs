using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    [Header("Settings")]
    public GameObject enemyPrefab;
    public Transform[] spawnPoints;
    public Transform targetToChase;
    public float spawnInterval = 3f;

    private bool isSpawning = true;

    void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        while (isSpawning)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnEnemy()
    {
        int index = Random.Range(0, spawnPoints.Length);
        Transform spawnPoint = spawnPoints[index];
        GameObject newEnemy = Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
        InfectedEnemy enemyScript = newEnemy.GetComponent<InfectedEnemy>();
        if (enemyScript != null)
        {
            enemyScript.target = targetToChase;
        }
    }

    public void StopSpawning()
    {
        isSpawning = false;
    }
}