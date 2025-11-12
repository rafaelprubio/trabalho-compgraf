using UnityEngine;
using System.Collections;

public class InfectedEnemy : MonoBehaviour
{
    [Header("States")]
    public bool isInfected = true;
    public bool isFrozen = false;

    [Header("Materials")]
    public Material infectedMaterial; // Red
    public Material curedMaterial;    // Blue

    [Header("Movement")]
    public float moveSpeed = 2f;
    public Transform target; // For Room 2: the civilian

    [Header("Audio")]
    public AudioClip curedSound;
    public AudioClip freezeSound;

    private Renderer rend;
    private AudioSource audioSource;
    private Rigidbody rb;

    void Start()
    {
        rend = GetComponent<Renderer>();
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
        rend.material = infectedMaterial;
    }

    void Update()
    {
        if (!isFrozen && isInfected && target != null)
        {
            // Move towards target (civilian in Room 2)
            Vector3 direction = (target.position - transform.position).normalized;
            transform.position += direction * moveSpeed * Time.deltaTime;
        }
    }

    public void GetCured()
    {
        if (!isInfected) return;

        isInfected = false;
        rend.material = curedMaterial;
        audioSource.PlayOneShot(curedSound);
        GameManager.Instance.AddScore(10);
        StartCoroutine(FadeAndDestroy());
    }

    public void GetFrozen(float duration)
    {
        if (isFrozen) return;
        StartCoroutine(FreezeCoroutine(duration));
    }

    IEnumerator FreezeCoroutine(float duration)
    {
        isFrozen = true;
        audioSource.PlayOneShot(freezeSound);
        
        // feedback visual
        rend.material.color = Color.cyan;
        yield return new WaitForSeconds(duration);
        isFrozen = false;
        if (isInfected)
            rend.material.color = Color.red;
    }

    IEnumerator FadeAndDestroy()
    {
        float elapsed = 0f;
        float fadeDuration = 2f;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsed / fadeDuration);
            Color color = rend.material.color;
            color.a = alpha;
            rend.material.color = color;
            yield return null;
        }

        Destroy(gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Civilian") && isInfected)
        {
            GameManager.Instance.GameOver();
        }
    }
}