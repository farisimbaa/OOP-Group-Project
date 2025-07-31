using UnityEngine;

public class CoinLevel6 : Pickup
{
    public int value = 5;
    public AudioClip coinSound;

    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(playerTag))
        {
            SoundManager.Instance.PlaySound(coinSound);
            ScoreSystemLevel6.Instance.AddScore(value);
            Destroy(gameObject);
        }
    }
}
