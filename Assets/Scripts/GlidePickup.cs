using UnityEngine;

public class GlidePickup : MonoBehaviour
{
    public string playerTag = "Player";
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(playerTag))
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
