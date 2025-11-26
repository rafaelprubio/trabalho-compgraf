using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InfectionHeart : MonoBehaviour
{
    [Header("Boss Stats")]
    public int maxHealth = 10;
    private int currentHealth;
    public bool hasShield = true;

    [Header("References")]
    public GameObject[] spawners;        
    public GameObject shieldVisual;      
    public Slider healthBar;             
    
    [Header("Model & Animation")]
    public SkinnedMeshRenderer bossBodyMesh; 
    public Animator bossAnimator;

    [Header("Phase 2: Boss Spawning")]
    public GameObject minionPrefab;
    public Transform minionSpawnPoint;
    public float bossSpawnInterval = 4f; 

    [Header("Visuals & Audio")]
    public Material curedMaterial; 
    public AudioClip bossMusic; 

    private Coroutine spawningCoroutine;

    void Start()
    {
        currentHealth = maxHealth;
        if (BackgroundMusic.Instance != null && bossMusic != null)
        {
            BackgroundMusic.Instance.ChangeMusic(bossMusic);
        }
        if (healthBar != null)
        {
            healthBar.maxValue = maxHealth;
            healthBar.value = currentHealth;
            healthBar.gameObject.SetActive(false); 
        }
    }

    void Update()
    {
        if (hasShield && AllSpawnersDestroyed())
        {
            DropShield();
        }
    }

    bool AllSpawnersDestroyed()
    {
        foreach (GameObject spawner in spawners)
        {
            if (spawner != null) return false;
        }
        return true;
    }

    void DropShield()
    {
        hasShield = false;
        if (shieldVisual != null) shieldVisual.SetActive(false);
        if (healthBar != null) healthBar.gameObject.SetActive(true);
        if (GameManager.Instance != null)
            GameManager.Instance.UpdateObjective("ESCUDO CAIU! ATAQUE O CHEFE!");
        spawningCoroutine = StartCoroutine(BossSpawnRoutine());
    }
    IEnumerator BossSpawnRoutine()
    {
        yield return new WaitForSeconds(2f);

        while (currentHealth > 0)
        {
            PlaySummonAnimation();
            yield return new WaitForSeconds(0.5f); 

            if (minionPrefab != null && minionSpawnPoint != null)
            {
                Instantiate(minionPrefab, minionSpawnPoint.position, Quaternion.identity);
            }

            yield return new WaitForSeconds(bossSpawnInterval);
        }
    }

    public void PlaySummonAnimation()
    {
        if (bossAnimator != null) bossAnimator.SetTrigger("Summon");
    }

    public void GetCured() 
    {
        TakeDamage(1);
    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (healthBar != null) healthBar.value = currentHealth;

        StartCoroutine(FlashColor());

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    IEnumerator FlashColor()
    {
        if (bossBodyMesh != null)
        {
            Color original = bossBodyMesh.material.color;
            bossBodyMesh.material.color = Color.cyan;
            yield return new WaitForSeconds(0.1f);
            bossBodyMesh.material.color = original;
        }
    }

    void Die()
    {
        if (spawningCoroutine != null) StopCoroutine(spawningCoroutine);

        if (bossAnimator != null) bossAnimator.SetTrigger("Die");
        if (healthBar != null) healthBar.gameObject.SetActive(false);

        if (bossBodyMesh != null && curedMaterial != null)
            bossBodyMesh.material = curedMaterial;

        RenderSettings.ambientLight = Color.white;
        GameManager.Instance.AddScore(500);
        GameManager.Instance.Victory();
    }
}