using UnityEngine;

public class EDrink : Pickup
{
    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(playerTag))
        {
            PlayerLevel6 player = other.GetComponent<PlayerLevel6>();
            if (player != null)
            {
                PowerupManager.Instance.StartSpeedBoost(duration, player);
                Destroy(gameObject);
            }
        }
    }
}