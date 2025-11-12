using UnityEngine;

public class MediCaster : MonoBehaviour
{
    [Header("Projectile Settings")]
    public GameObject cureDartPrefab;
    public GameObject quarantineGelPrefab;
    public Transform firePoint;
    public float projectileSpeed = 20f;

    [Header("Audio")]
    public AudioClip cureSound;
    public AudioClip quarantineSound;
    private AudioSource audioSource;

    [Header("Cooldowns")]
    public float cureCooldown = 0.5f;
    public float quarantineCooldown = 2f;
    private float lastCureTime;
    private float lastQuarantineTime;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // Botao esquerdo
        if (Input.GetMouseButtonDown(0) && Time.time >= lastCureTime + cureCooldown)
        {
            ShootCureDart();
            lastCureTime = Time.time;
        }

        // Botao direito
        if (Input.GetMouseButtonDown(1) && Time.time >= lastQuarantineTime + quarantineCooldown)
        {
            ShootQuarantineGel();
            lastQuarantineTime = Time.time;
        }
    }

    void ShootCureDart()
    {
        GameObject dart = Instantiate(cureDartPrefab, firePoint.position, firePoint.rotation);
        dart.GetComponent<Rigidbody>().linearVelocity = firePoint.forward * projectileSpeed;
        audioSource.PlayOneShot(cureSound);
        Destroy(dart, 5f);
    }

    void ShootQuarantineGel()
    {
        GameObject gel = Instantiate(quarantineGelPrefab, firePoint.position, firePoint.rotation);
        gel.GetComponent<Rigidbody>().linearVelocity = firePoint.forward * projectileSpeed;
        audioSource.PlayOneShot(quarantineSound);
        Destroy(gel, 5f);
    }
}