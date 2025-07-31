using UnityEngine;

public class EDrink : Pickup
{
    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(playerTag))
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                PowerupManager.Instance.StartSpeedBoost(duration, player);
                Destroy(gameObject);
            }
        }
    }
}