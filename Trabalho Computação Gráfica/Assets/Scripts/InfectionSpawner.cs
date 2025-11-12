using UnityEngine;

public class InfectionSpawner : MonoBehaviour
{
    [Header("Spawning")]
    public GameObject infectedPrefab;
    public float spawnInterval = 5f;
    public Transform spawnPoint;

    [Header("Health")]
    public int health = 5;
    public Material damagedMaterial;

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
        Instantiate(infectedPrefab, spawnPoint.position, Quaternion.identity);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        // feedback visual
        rend.material.color = Color.Lerp(Color.red, Color.white, (float)health / 5f);
        if (health <= 0)
        {
            DestroySpawner();
        }
    }

    void DestroySpawner()
    {
        isDestroyed = true;
        Destroy(gameObject);
    }
}