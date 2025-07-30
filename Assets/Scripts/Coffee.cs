using UnityEngine;

public class Coffee : Pickup
{
    public string playerTag = "Player";
    public AudioClip coffeeSound;

    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(playerTag))
        {
            PowerupManager.Instance.StartJumpBoost(5f);
            Destroy(gameObject);
        }
    }
}