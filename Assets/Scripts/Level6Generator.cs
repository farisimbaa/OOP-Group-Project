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
    public GameObject rocketPrefab; //2%
    public GameObject coffeePrefab; // 2%
    public GameObject EDrinkPrefab; // 2%

    //Multipickups:
    public GameObject plusFivePrefab; //1%
    public GameObject multPointFivePrefab; //1%
    public GameObject multOnePointFivePrefab; //1%

    protected float rocketSpawnChance = 0.02f;
    protected float coffeeSpawnChance = 0.02f;
    protected float EDrinkSpawnChance = 0.02f;
    protected float multSpawnChance = 0.01f;

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

        if (!spawnedPickup && Random.value < coinSpawnChance && coinPrefab != null)
        {
            Vector3 coinSpawnPos = new Vector3(spawnX, spawnY + 0.3f, 0);
            GameObject coin = Instantiate(coinPrefab, coinSpawnPos, Quaternion.identity);
            coin.transform.parent = platform.transform;
            spawnedPickup = true;
        }

        if (!spawnedPickup && Random.value < rocketSpawnChance && rocketPrefab != null)
        {
            Vector3 rocketSpawnPos = new Vector3(spawnX, spawnY + 0.3f, 0);
            GameObject rocket = Instantiate(rocketPrefab, rocketSpawnPos, Quaternion.identity);
            rocket.transform.parent = platform.transform;
            spawnedPickup = true;
        }

        if (!spawnedPickup && Random.value < EDrinkSpawnChance && EDrinkPrefab != null)
        {
            Vector3 EDrinkSpawnPos = new Vector3(spawnX, spawnY + 0.3f, 0);
            GameObject EDrink = Instantiate(EDrinkPrefab, EDrinkSpawnPos, Quaternion.identity);
            EDrink.transform.parent = platform.transform;
            spawnedPickup = true;
        }

        if (!spawnedPickup && Random.value < coffeeSpawnChance && coffeePrefab != null)
        {
            Vector3 coffeeSpawnPos = new Vector3(spawnX, spawnY + 0.3f, 0);
            GameObject coffee = Instantiate(coffeePrefab, coffeeSpawnPos, Quaternion.identity);
            coffee.transform.parent = platform.transform;
            spawnedPickup = true;
        }

        if (!spawnedPickup && TrySpawnMultiplier(spawnX, spawnY, platform))
        {
            spawnedPickup = true;
        }

        highestY = spawnY;
    }

    void SpawnPickup(GameObject prefab, float x, float y, GameObject platform)
    {
        Vector3 pos = new Vector3(x, y + 0.3f, 0);
        GameObject pickup = Instantiate(prefab, pos, Quaternion.identity);
        pickup.transform.parent = platform.transform;
    }
    
    bool TrySpawnMultiplier(float x, float y, GameObject platform)
    {
        float roll = Random.value;
        if (roll < multSpawnChance && plusFivePrefab != null)
        {
            SpawnPickup(plusFivePrefab, x, y, platform);
            return true;
        }
        else if (roll < 2 * multSpawnChance && multOnePointFivePrefab != null)
        {
            SpawnPickup(multOnePointFivePrefab, x, y, platform);
            return true;
        }
        else if (roll < 3 * multSpawnChance && multPointFivePrefab != null)
        {
            SpawnPickup(multPointFivePrefab, x, y, platform);
            return true;
        }

        return false;
    }
}
