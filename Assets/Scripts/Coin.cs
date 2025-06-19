using UnityEngine;

public class Coin : MonoBehaviour
{
    public string playerTag = "Player";
    public int value = 5;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(playerTag))
        {
            ScoreSystem.Instance.AddScore(value);
            Destroy(gameObject);
        }
    }
}
