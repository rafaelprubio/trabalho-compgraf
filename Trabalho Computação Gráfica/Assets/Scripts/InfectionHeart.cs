using UnityEngine;

public class InfectionHeart : MonoBehaviour
{
    public GameObject[] spawners;
    public bool hasShield = true;
    public GameObject shieldVisual;
    public Material curedMaterial;

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
            if (spawner != null)
                return false;
        }
        return true;
    }

    void DropShield()
    {
        hasShield = false;
        shieldVisual.SetActive(false);
    }

    public void GetCured()
    {
        GetComponent<Renderer>().material = curedMaterial;
        RenderSettings.ambientLight = Color.white;
        GameManager.Instance.AddScore(100);
        GameManager.Instance.Victory();
    }
}