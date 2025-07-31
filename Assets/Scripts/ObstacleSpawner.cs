using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject obstaclePrefab;
    public int obstacleCount = 5; // Only 5 obstacles max
    public float minX = -3f;
    public float maxX = 3f;
    public float minY = 1f;
    public float maxY = 10f;

    void Start()
    {
        SpawnObstacles();
    }

    void SpawnObstacles()
    {
        for (int i = 0; i < obstacleCount; i++)
        {
            Vector2 spawnPos = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
            Instantiate(obstaclePrefab, spawnPos, Quaternion.identity);
        }
    }
}
