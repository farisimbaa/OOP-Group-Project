using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformDefault : Platform
{
    public AudioClip jumpSound;

    public override void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.relativeVelocity.y <= 0f)
        {
            Rigidbody2D rb = collision.collider.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                Vector2 velocity = rb.linearVelocity;
                velocity.y = EffectiveJumpForce;
                rb.linearVelocity = velocity;
                SoundManager.Instance.PlaySound(jumpSound);
            }
        }
    }
}
