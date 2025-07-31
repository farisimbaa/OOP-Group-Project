using UnityEngine;

public class Level5Generator : MonoBehaviour
{
    [Header("Platform Prefabs")]
    public GameObject defaultPlatformPrefab;   // 85%
    public GameObject stickyPlatformPrefab;    // 15%

    [Header("Pickup Prefabs")]
    public GameObject coinPrefab;              // 10%
    public GameObject glidePickupPrefab;       // 5%

    [Header("References")]
    public Transform player;
    public SpriteRenderer background;

    [Header("Spawn Settings")]
    public float minY = 0.8f;
    public float maxY = 2.0f;
    public float verticalBuffer = 5f;

    private float highestY;
    private float minX;
    private float maxX;

    void Start()
    {
        // Set horizontal bounds based on background
        minX = background.bounds.min.x;
        maxX = background.bounds.max.x;

        highestY = player.position.y;

        // Spawn initial platforms
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

        // Platform selection
        float rand = Random.value;
        if (rand < 0.15f)
            Instantiate(stickyPlatformPrefab, spawnPos, Quaternion.identity);
        else
            Instantiate(defaultPlatformPrefab, spawnPos, Quaternion.identity);

        // Coin chance
        if (Random.value < 0.10f && coinPrefab != null)
        {
            Vector3 coinPos = spawnPos + Vector3.up * 0.5f;
            Instantiate(coinPrefab, coinPos, Quaternion.identity);
        }

        // Glide pickup chance
        if (Random.value < 0.05f && glidePickupPrefab != null)
        {
            Vector3 glidePos = spawnPos + Vector3.up * 0.6f;
            Instantiate(glidePickupPrefab, glidePos, Quaternion.identity);
        }

        highestY = spawnY;
    }
}
