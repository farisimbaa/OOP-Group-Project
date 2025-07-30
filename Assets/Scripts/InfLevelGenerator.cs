using UnityEngine;

public class InfLevelGenerator : MonoBehaviour
{
    public GameObject defaultPlatformPrefab; //80%
    public GameObject movingPlatformPrefab; //5%
    public GameObject trampolinePlatformPrefab; //5%
    public GameObject breakPlatformPrefab; //5%
    public GameObject spikePlatformPrefab; //5%
    public GameObject coinPrefab;
    public SpriteRenderer background;

    public Transform player;
    public float minY;
    public float maxY;
    public float coinSpawnChance = 0.05f;
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

        GameObject platformPrefab;

        float chance = Random.value;
        if (chance < 0.8f)
        {
            platformPrefab = defaultPlatformPrefab;
        }
        else if (chance < 0.85f)
        {
            platformPrefab = movingPlatformPrefab;
        }
        else if (chance < 0.9f)
        {
            platformPrefab = trampolinePlatformPrefab;
        }
        else if (chance < 0.95f)
        {
            platformPrefab = breakPlatformPrefab;
        }
        else
        {
            platformPrefab = spikePlatformPrefab;
        }
        
        Instantiate(platformPrefab, spawnPos, Quaternion.identity);

        if (Random.value < coinSpawnChance && coinPrefab != null)
        {
            Vector3 coinSpawnPos = new Vector3(spawnX, spawnY + 0.3f, 0);
            Instantiate(coinPrefab, coinSpawnPos, Quaternion.identity);
        }

        highestY = spawnY;
    }
}
