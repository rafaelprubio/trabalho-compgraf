using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class InfectedEnemy : MonoBehaviour
{
    [Header("States")]
    public bool isInfected = true;
    public bool isFrozen = false;

    [Header("Materials")]
    public Material infectedMaterial;
    public Material curedMaterial;

    [Header("Movement")]
    public Transform target; 

    [Header("Audio")]
    public AudioClip curedSound;
    public AudioClip freezeSound;

    private Renderer rend;
    private AudioSource audioSource;
    private NavMeshAgent agent;
    private Color originalInfectedColor; 

    void Start()
    {
        rend = GetComponent<Renderer>();
        audioSource = GetComponent<AudioSource>();
        agent = GetComponent<NavMeshAgent>();
        if (rend != null && infectedMaterial != null)
        {
            rend.material = infectedMaterial;
            originalInfectedColor = rend.material.color;
        }
        if (target == null && GameObject.FindGameObjectWithTag("Player"))
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    void Update()
    {
        if (isInfected && !isFrozen && target != null)
        {
            if (agent != null)
            {
                if (agent.isStopped) agent.isStopped = false;
                agent.SetDestination(target.position);
            }
        }
        else
        {
            if (agent != null) agent.isStopped = true;
        }
    }

    public void GetCured()
    {
        if (!isInfected) return;

        isInfected = false;
        if(agent != null) agent.isStopped = true; 
        if(rend != null) rend.material = curedMaterial;
        
        if(audioSource && curedSound) audioSource.PlayOneShot(curedSound);
        
        if (GameManager.Instance != null) 
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
        if(agent != null) 
        {
            agent.isStopped = true;
            agent.ResetPath();
        }

        if(audioSource && freezeSound) audioSource.PlayOneShot(freezeSound);
        if(rend != null) rend.material.color = Color.cyan;

        yield return new WaitForSeconds(duration);
        isFrozen = false;
        if (isInfected && rend != null)
        {
            rend.material.color = originalInfectedColor; 
        }
    }

    IEnumerator FadeAndDestroy()
    {
        float elapsed = 0f;
        float fadeDuration = 2f;
        Color startColor = rend.material.color; 

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsed / fadeDuration);
            Color newColor = new Color(startColor.r, startColor.g, startColor.b, alpha);
            rend.material.color = newColor;
            
            yield return null;
        }

        Destroy(gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        if ((other.CompareTag("Civilian") || other.CompareTag("Player")) && isInfected)
        {
            if(GameManager.Instance != null) 
            {
                GameManager.Instance.GameOver();
            }
        }
    }
}