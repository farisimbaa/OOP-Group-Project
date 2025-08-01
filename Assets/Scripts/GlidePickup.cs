using UnityEngine;

public class GlidePickup : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerX player = other.GetComponent<PlayerX>();
            if (player != null)
            {
                player.ActivateGlide();
            }

            Destroy(gameObject); // remove the pickup after use
        }
    }
}
