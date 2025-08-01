using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public GameObject monsterPrefab;
    public float minX = -2.5f;
    public float maxX = 2.5f;
    public float minYSpacing = 2f;
    public float maxYSpacing = 4f;
    public int numberOfMonsters = 10; // Total number of monsters to place

    private float lastY = 0f;

    void Start()
    {
        lastY = transform.position.y;

        for (int i = 0; i < numberOfMonsters; i++)
        {
            SpawnMonster();
        }
    }

    void SpawnMonster()
    {
        float x = Random.Range(minX, maxX);
        float y = lastY + Random.Range(minYSpacing, maxYSpacing);
        lastY = y;

        Vector2 spawnPos = new Vector2(x, y);
        Instantiate(monsterPrefab, spawnPos, Quaternion.identity);
    }
}
