using System.Collections;
using UnityEngine;

public class PlatformMoving : Platform
{
    public float moveSpeed = 2f;
    private float minX, maxX;
    private int direction = 1; // 1 = right, -1 = left
    public AudioClip jumpSound;

    void Update()
    {
        transform.Translate(Vector2.right * direction * moveSpeed * Time.deltaTime);

        // Reverse direction if it hits bounds
        if (transform.position.x < minX || transform.position.x > maxX)
        {
            direction *= -1;
            float clampedX = Mathf.Clamp(transform.position.x, minX, maxX);
            transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);
        }
    }

    public void Start()
    {

        SpriteRenderer background = GameObject.Find("Background").GetComponent<SpriteRenderer>();
        minX = background.bounds.min.x;
        maxX = background.bounds.max.x;
    }

    public void OnCollisionEnter2D(Collision2D collision)
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
