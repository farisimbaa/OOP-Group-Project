using UnityEngine;

public abstract class ScoreMultiplierPickup : Pickup
{
    public string playerTag = "Player";
    public AudioClip pickupSound;

    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(playerTag))
        {
            SoundManager.Instance.PlaySound(pickupSound);
            ApplyEffect();
            Destroy(gameObject);
        }
    }

    protected abstract void ApplyEffect();
}
