using System.Collections;
using UnityEngine;

public class PlatformBreak : Platform
{
    public Sprite defaultSprite;
    public Sprite brokenSprite;
    public float destroyDelay = 0.3f;
    public AudioClip breakSound;

    private SpriteRenderer spriteRenderer;

    private bool isBroken = false;

    public void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        Collider2D platformCollider = GetComponent<Collider2D>();

        isBroken = false;

        if (spriteRenderer != null && defaultSprite != null)
        {
            spriteRenderer.sprite = defaultSprite;
        }

        if (platformCollider != null)
        {
            platformCollider.enabled = true;
        }

    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (isBroken) return;

        if (collision.relativeVelocity.y <= 0f)
        {
            isBroken = true;

            Collider2D collider = GetComponent<Collider2D>();
            if (collider != null)
                collider.enabled = false;

            if (spriteRenderer != null && brokenSprite != null)
            {
                spriteRenderer.sprite = brokenSprite;
                SoundManager.Instance.PlaySound(breakSound);
            }
        }
    }
}