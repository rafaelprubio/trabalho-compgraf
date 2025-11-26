using UnityEngine;

public class InfectionSpawner : MonoBehaviour
{
    [Header("Spawning")]
    public GameObject infectedPrefab;
    public float spawnInterval = 5f;
    public Transform spawnPoint;

    [Header("Boss Connection")]
    public InfectionHeart bossScript; 

    [Header("Health")]
    public int health = 5;

    private float nextSpawnTime;
    private Renderer rend;
    private bool isDestroyed = false;

    void Start()
    {
        rend = GetComponent<Renderer>();
        nextSpawnTime = Time.time + spawnInterval;
    }

    void Update()
    {
        if (!isDestroyed && Time.time >= nextSpawnTime)
        {
            SpawnInfected();
            nextSpawnTime = Time.time + spawnInterval;
        }
    }

    void SpawnInfected()
    {
        if (infectedPrefab != null && spawnPoint != null)
        {
            Instantiate(infectedPrefab, spawnPoint.position, Quaternion.identity);
        }
        if (bossScript != null)
        {
            bossScript.PlaySummonAnimation();
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        
        if (rend != null)
            rend.material.color = Color.Lerp(Color.red, Color.white, (float)health / 5f);
            
        if (health <= 0)
        {
            isDestroyed = true;
            Destroy(gameObject);
        }
    }
}