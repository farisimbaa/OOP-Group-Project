using UnityEngine;

public class DoubleJumpPickup : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerX playerScript = other.GetComponent<PlayerX>();
            if (playerScript != null)
            {
                playerScript.ActivateDoubleJump(); // Trigger double jump
            }
            Destroy(gameObject); // Remove pickup
        }
    }
}
