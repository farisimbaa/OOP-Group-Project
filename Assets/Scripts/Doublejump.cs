using UnityEngine;

public class DoubleJumpPickup : MonoBehaviour
{
    public string playerTag = "Player";
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(playerTag))
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
