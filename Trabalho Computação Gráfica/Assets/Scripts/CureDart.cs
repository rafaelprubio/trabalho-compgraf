using UnityEngine;

public class CureDart : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        InfectedEnemy enemy = other.GetComponent<InfectedEnemy>();
        if (enemy != null && enemy.isInfected)
        {
            enemy.GetCured();
            Destroy(gameObject);
        }
        
        InfectionSpawner spawner = other.GetComponent<InfectionSpawner>();
        if (spawner != null)
        {
            spawner.TakeDamage(1);
            Destroy(gameObject);
        }

        InfectionHeart heart = other.GetComponent<InfectionHeart>();
        if (heart != null && !heart.hasShield)
        {
            heart.GetCured();
            Destroy(gameObject);
        }
    }
}