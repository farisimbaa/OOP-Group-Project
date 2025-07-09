using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformTrampoline : Platform
{
    public float jumpForce = 10f;
    public override void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public override void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.relativeVelocity.y <= 0f)
        {
            Rigidbody2D rb = collision.collider.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                Vector2 velocity = rb.linearVelocity;
                velocity.y = jumpForce;
                rb.linearVelocity = velocity;
                audioSource.PlayOneShot(jumpSound);
            }
        }
    }
}
