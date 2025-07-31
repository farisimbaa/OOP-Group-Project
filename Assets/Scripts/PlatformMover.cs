using UnityEngine;

public class PlatformMover : MonoBehaviour
{
    public GameObject platformPrefab;      // Your platform prefab
    public GameObject obstaclePrefab;      // Your obstacle prefab

    public float platformSpacingY = 2f;    // Vertical spacing between platforms
    public int initialPlatforms = 10;      // Number of platforms at start

    private float lastPlatformY;

    void Start()
    {
        lastPlatformY = transform.position.y;

        for (int i = 0; i < initialPlatforms; i++)
        {
            SpawnPlatform();
        }
    }

    void SpawnPlatform()
    {
        Vector3 spawnPosition = new Vector3(Random.Range(-2f, 2f), lastPlatformY + platformSpacingY, 0f);
        GameObject platform = Instantiate(platformPrefab, spawnPosition, Quaternion.identity);

        // 30% chance to spawn obstacle
        if (Random.value < 0.5f)
    {
        Vector3 obstaclePosition = spawnPosition + new Vector3(0f, 0.5f, 0f);
        GameObject obstacle = Instantiate(obstaclePrefab, obstaclePosition, Quaternion.identity);

        // Add movement if not already in prefab
        if (obstacle.GetComponent<ObstacleMover>() == null)
        {
            obstacle.AddComponent<ObstacleMover>();
        }
    }

        lastPlatformY = spawnPosition.y;
    }

    // You can call this method later if you want to spawn more platforms as player goes up
    public void SpawnNextPlatform()
    {
        SpawnPlatform();
    }
}
