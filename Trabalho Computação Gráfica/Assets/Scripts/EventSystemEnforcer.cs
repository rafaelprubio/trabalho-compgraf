using UnityEngine;
using UnityEngine.EventSystems;

public class EventSystemEnforcer : MonoBehaviour
{
    void Awake()
    {
        EventSystem[] systems = FindObjectsOfType<EventSystem>();
        if (systems.Length > 1)
        {
            Destroy(gameObject);
        }
    }
}