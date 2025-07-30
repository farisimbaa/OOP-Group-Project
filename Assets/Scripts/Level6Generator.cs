using UnityEngine;

public class Level6Generator : LevelGenerator
{
    //Platforms:
    public GameObject defaultPlatformPrefab; //80%
    public GameObject movingPlatformPrefab; //5%
    public GameObject trampolinePlatformPrefab; //5%
    public GameObject breakPlatformPrefab; //5%
    public GameObject spikePlatformPrefab; //5%

    //Pickups:
    public GameObject rocketPrefab; //5%

    protected float rocketSpawnChance = 0.02f;

    void Start()
    {
        coinSpawnChance = 0.05f; // Adjusted coin spawn chance for Level 6

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
        
        GameObject platform = Instantiate(platformPrefab, spawnPos, Quaternion.identity);

        bool spawnedPickup = false;

        if (!spawnedPickup && Random.value < rocketSpawnChance && rocketPrefab != null)
    {
        Vector3 rocketSpawnPos = new Vector3(spawnX, spawnY + 0.3f, 0);
        GameObject rocket = Instantiate(rocketPrefab, rocketSpawnPos, Quaternion.identity);

        rocket.transform.parent = platform.transform;

        spawnedPickup = true;
    }

        if (!spawnedPickup && Random.value < coinSpawnChance && coinPrefab != null)
    {
        Vector3 coinSpawnPos = new Vector3(spawnX, spawnY + 0.3f, 0);
        GameObject coin = Instantiate(coinPrefab, coinSpawnPos, Quaternion.identity);

        coin.transform.parent = platform.transform;

        spawnedPickup = true;
    }

        highestY = spawnY;
    }
}
