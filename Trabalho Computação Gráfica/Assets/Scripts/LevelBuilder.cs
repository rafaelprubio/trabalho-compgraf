using UnityEngine;

public class LevelBuilder : MonoBehaviour
{
    [Header("Floor Prefabs")]
    public GameObject floorTilePrefab;
    public GameObject wallPrefab;
    
    [Header("Room Configuration")]
    public int roomWidth = 10;
    public int roomDepth = 10;
    public float tileSize = 4f;
    public float wallHeight = 4f;
    
    [Header("Props")]
    public GameObject[] decorationPrefabs;
    public int numberOfDecorations = 5;
    
    void Start()
    {
        BuildRoom();
    }
    
    void BuildRoom()
    {
        for (int x = 0; x < roomWidth; x++)
        {
            for (int z = 0; z < roomDepth; z++)
            {
                Vector3 pos = new Vector3(x * tileSize, 0, z * tileSize);
                GameObject tile = Instantiate(floorTilePrefab, pos, Quaternion.identity, transform);
                tile.name = $"Floor_{x}_{z}";
            }
        }
        BuildWalls();
        if (decorationPrefabs != null && decorationPrefabs.Length > 0)
        {
            PlaceDecorations();
        }
    }
    
    void BuildWalls()
    {
        for (int x = 0; x < roomWidth; x++)
        {
            Vector3 pos = new Vector3(x * tileSize, wallHeight / 2, roomDepth * tileSize);
            CreateWall(pos, Vector3.zero);
        }
        for (int x = 0; x < roomWidth; x++)
        {
            Vector3 pos = new Vector3(x * tileSize, wallHeight / 2, -tileSize);
            CreateWall(pos, Vector3.zero);
        }
        for (int z = 0; z < roomDepth; z++)
        {
            Vector3 pos = new Vector3(roomWidth * tileSize, wallHeight / 2, z * tileSize);
            CreateWall(pos, new Vector3(0, 90, 0));
        }
        for (int z = 0; z < roomDepth; z++)
        {
            Vector3 pos = new Vector3(-tileSize, wallHeight / 2, z * tileSize);
            CreateWall(pos, new Vector3(0, 90, 0));
        }
    }
    
    void CreateWall(Vector3 position, Vector3 rotation)
    {
        if (wallPrefab != null)
        {
            GameObject wall = Instantiate(wallPrefab, position, Quaternion.Euler(rotation), transform);
            wall.transform.localScale = new Vector3(tileSize, wallHeight, 0.5f);
        }
        else
        {
            GameObject wall = GameObject.CreatePrimitive(PrimitiveType.Cube);
            wall.transform.position = position;
            wall.transform.rotation = Quaternion.Euler(rotation);
            wall.transform.localScale = new Vector3(tileSize, wallHeight, 0.5f);
            wall.transform.parent = transform;
            wall.name = "Wall";
        }
    }
    
    void PlaceDecorations()
    {
        for (int i = 0; i < numberOfDecorations; i++)
        {
            float x = Random.Range(1, roomWidth - 1) * tileSize;
            float z = Random.Range(1, roomDepth - 1) * tileSize;
            Vector3 pos = new Vector3(x, 0, z);
            GameObject prefab = decorationPrefabs[Random.Range(0, decorationPrefabs.Length)];
            Instantiate(prefab, pos, Quaternion.identity, transform);
        }
    }
}