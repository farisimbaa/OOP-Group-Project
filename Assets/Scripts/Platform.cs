using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Platform : MonoBehaviour
{
<<<<<<< HEAD
    public float jumpForce = 10f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.relativeVelocity.y <= 0f)
        {
            Rigidbody2D rb = collision.collider.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            Vector2 velocity = rb.linearVelocity;
            velocity.y = jumpForce;
            rb.linearVelocity = velocity;
        }
        }
    }
    
=======
    public float baseJumpForce = 5.5f;
    public static float jumpForceMultiplier = 1f;
    public float EffectiveJumpForce => baseJumpForce * jumpForceMultiplier;
    
    public abstract void OnCollisionEnter2D(Collision2D collision);
>>>>>>> main
}
