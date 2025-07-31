using UnityEngine;

public class Level5Generator : LevelGenerator
{
    [Header("Platform Prefabs")]
    public GameObject defaultPlatformPrefab;   // 85%
    public GameObject stickyPlatformPrefab;    // 15%

    [Header("Pickup Prefabs")]
    public GameObject coinPrefab;             // 10%
    public GameObject glidePickupPrefab;      // 5%

    protected override void SpawnNextPlatform()
    {
        float spawnY = highestY + Random.Range(minY, maxY);
        float spawnX = Random.Range(minX, maxX);
        Vector3 spawnPos = new Vector3(spawnX, spawnY, 0);

        float rand = Random.value;

        // Platform selection
        if (rand < 0.15f)
            Instantiate(stickyPlatformPrefab, spawnPos, Quaternion.identity);
        else
            Instantiate(defaultPlatformPrefab, spawnPos, Quaternion.identity);

        // Pickup spawn chance
        float coinChance = Random.value;
        if (coinChance < 0.10f && coinPrefab != null)
        {
            Vector3 coinPos = spawnPos + Vector3.up * 0.5f;
            Instantiate(coinPrefab, coinPos, Quaternion.identity);
        }

        float glideChance = Random.value;
        if (glideChance < 0.05f && glidePickupPrefab != null)
        {
            Vector3 glidePos = spawnPos + Vector3.up * 0.6f;
            Instantiate(glidePickupPrefab, glidePos, Quaternion.identity);
        }

        highestY = spawnY;
    }
}
