using UnityEngine;

public class Level3Generator : MonoBehaviour
{
    public GameObject platformPrefab;
    public GameObject spikePrefab;
    public GameObject coinPrefab;
    public SpriteRenderer background;
    public Transform player;

    public int initialPlatformCount = 50;
    public float minYSpacing = 0.05f;
    public float maxYSpacing = 0.12f;

    public float verticalBuffer = 6f;
    public float spikeSpawnChance = 0.9f;
    public float coinSpawnChance = 0.4f;

    private float minX;
    private float maxX;
    private float highestY;

    void Start()
    {
        if (player == null)
        {
            Debug.LogError("Player not assigned to Level3Generator.");
            return;
        }

        highestY = player.position.y;

        if (background != null)
        {
            minX = background.bounds.min.x + 0.5f;
            maxX = background.bounds.max.x - 0.5f;
        }
        else
        {
            minX = -2.5f;
            maxX = 2.5f;
        }

        for (int i = 0; i < initialPlatformCount; i++)
        {
            SpawnNextSet();
        }
    }

    void Update()
    {
        if (player.position.y + verticalBuffer > highestY)
        {
            for (int i = 0; i < 5; i++)
            {
                SpawnNextSet();
            }
        }
    }

    void SpawnNextSet()
    {
        float spawnY = highestY + Random.Range(minYSpacing, maxYSpacing);

        int platformsPerLayer = 3;
        for (int i = 0; i < platformsPerLayer; i++)
        {
            float spawnX = Random.Range(minX, maxX);
            Vector3 platformPos = new Vector3(spawnX, spawnY, 0f);

            if (!Physics2D.OverlapCircle(platformPos, 0.3f))
            {
                Instantiate(platformPrefab, platformPos, Quaternion.identity);

                if (coinPrefab != null && Random.value < coinSpawnChance)
                {
                    Vector3 coinPos = new Vector3(spawnX, spawnY + 0.6f, 0f);
                    if (!Physics2D.OverlapCircle(coinPos, 0.2f))
                    {
                        Instantiate(coinPrefab, coinPos, Quaternion.identity);
                    }
                }
            }
        }

        int spikeAttempts = 5;
        for (int i = 0; i < spikeAttempts; i++)
        {
            if (spikePrefab != null && Random.value < spikeSpawnChance)
            {
                float spikeX = Random.Range(minX, maxX);
                float spikeY = spawnY + Random.Range(-0.1f, 0.4f);
                Vector3 spikePos = new Vector3(spikeX, spikeY, 0f);

                if (!Physics2D.OverlapCircle(spikePos, 0.3f))
                {
                    Instantiate(spikePrefab, spikePos, Quaternion.identity);
                }
            }
        }

        highestY = spawnY;
    }
}
