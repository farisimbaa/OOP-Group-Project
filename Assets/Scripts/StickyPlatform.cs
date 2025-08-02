using UnityEngine;

public class StickyPlatform : MonoBehaviour
{
    public string playerTag = "Player";
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(playerTag))
        {
            PlayerX movement = collision.gameObject.GetComponent<PlayerX>();
            if (movement != null)
            {
                movement.canJump = false;
            }
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(playerTag))
        {
            PlayerX movement = collision.gameObject.GetComponent<PlayerX>();
            if (movement != null)
            {
                movement.canJump = true;
            }
        }
    }
}