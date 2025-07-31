using UnityEngine;

public class ObstacleMover : MonoBehaviour
{
    public float moveSpeed = 2f;   // How fast it moves
    public float moveRange = 1f;   // How far left/right it moves

    private Vector3 startPos;

    void Start()
    {
        // Set the starting position to wherever this obstacle was spawned
        startPos = transform.position;
    }

    void Update()
    {
        // Safety: make sure startPos is not zero due to prefab issues
        if (startPos == Vector3.zero)
        {
            startPos = transform.position;
        }

        // Move left and right using sine wave
        float offset = Mathf.Sin(Time.time * moveSpeed) * moveRange;
        transform.position = new Vector3(startPos.x + offset, startPos.y, startPos.z);

        // Optional: debug movement in console
        // Debug.Log("Obstacle X: " + transform.position.x);
    }
}