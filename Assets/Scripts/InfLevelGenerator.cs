using UnityEngine;

public class InfLevelGenerator : MonoBehaviour
{
    public GameObject platformPrefab;
    public GameObject coinPrefab;
    public SpriteRenderer background;

    public Transform player;
    public float minY;
    public float maxY;
    float minX;
    float maxX;
    public float verticalBuffer = 5f;

    private float highestY;

    void Start()
    {
        minX = background.bounds.min.x;
        maxX = background.bounds.max.x;

        highestY = player.position.y;

        for (int i = 0; i < 10; i++)
        {
            SpawnNextPlatform();
        }
    }

    void Update()
    {
        if (player.position.y + verticalBuffer > highestY)
        {
            SpawnNextPlatform();
        }
    }

    void SpawnNextPlatform()
    {
        float spawnY = highestY + Random.Range(minY, maxY);
        float spawnX = Random.Range(minX, maxX);

        Vector3 spawnPos = new Vector3(spawnX, spawnY, 0);
        Instantiate(platformPrefab, spawnPos, Quaternion.identity);

        if (Random.value < 0.05f && coinPrefab != null)
        {
            Vector3 coinSpawnPos = new Vector3(spawnX, spawnY + 0.3f, 0);
            Instantiate(coinPrefab, coinSpawnPos, Quaternion.identity);
        }

        highestY = spawnY;
    }
}
