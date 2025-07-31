using UnityEngine;

public class Coin : Pickup
{
    public int value = 5;
    public AudioClip coinSound;

    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(playerTag))
        {
            SoundManager.Instance.PlaySound(coinSound);
            ScoreSystem.Instance.AddScore(value);
            Destroy(gameObject);
        }
    }
}
