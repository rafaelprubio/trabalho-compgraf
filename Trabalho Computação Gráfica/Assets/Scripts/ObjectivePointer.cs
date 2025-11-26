using UnityEngine;
using UnityEngine.UI;

public class ObjectivePointer : MonoBehaviour
{
    [Header("Settings")]
    public Transform target; 
    public Transform player; 
    public float rotationSpeed = 10f;

    private RectTransform arrowRect;
    private Image arrowImage;

    void Start()
    {
        arrowRect = GetComponent<RectTransform>();
        arrowImage = GetComponent<Image>();
        if (player == null)
        {
            GameObject p = GameObject.FindGameObjectWithTag("Player");
            if (p != null) player = p.transform;
        }
    }

    void LateUpdate()
    {
        if (target == null || !target.gameObject.activeInHierarchy) 
        {
            if (arrowImage.enabled) arrowImage.enabled = false;
            return; 
        }
        else
        {
            if (!arrowImage.enabled) arrowImage.enabled = true;
        }

        if (player == null) return;
        Vector3 directionToTarget = target.position - player.position;
        Vector3 localDirection = player.InverseTransformDirection(directionToTarget);
        float angle = Mathf.Atan2(localDirection.x, localDirection.z) * Mathf.Rad2Deg;

        Quaternion targetRotation = Quaternion.Euler(0, 0, -angle);
        arrowRect.rotation = Quaternion.Lerp(arrowRect.rotation, targetRotation, Time.deltaTime * rotationSpeed);
    }
}