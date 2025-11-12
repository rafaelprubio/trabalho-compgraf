using UnityEngine;

public class QuarantineGel : MonoBehaviour
{
    public float freezeDuration = 3f;

    void OnTriggerEnter(Collider other)
    {
        InfectedEnemy enemy = other.GetComponent<InfectedEnemy>();
        if (enemy != null)
        {
            enemy.GetFrozen(freezeDuration);
            Destroy(gameObject);
        }
    }
}