using UnityEngine;

public class Coffee : Pickup
{
    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(playerTag))
        {
            PowerupManager.Instance.StartJumpBoost(duration);
            Destroy(gameObject);
        }
    }
}