using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMainMenu : Platform
{
    public AudioClip jumpSound;
    private AudioSource audioSource;

    public void Start()
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
                velocity.y = 8f;
                rb.linearVelocity = velocity;
                audioSource.PlayOneShot(jumpSound);
            }
        }
    }
}