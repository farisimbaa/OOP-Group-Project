using UnityEngine;

public class StickyPlatform : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
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
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerX movement = collision.gameObject.GetComponent<PlayerX>();
            if (movement != null)
            {
                movement.canJump = true;
            }
        }
    }
}