using System.Collections;
using UnityEngine;

public class PlatformMoving : Platform
{
    public float jumpForce = 5.5f;
    public float moveSpeed = 2f;
    private float minX, maxX;
    private int direction = 1; // 1 = right, -1 = left

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

    public override void Start()
    {
        audioSource = GetComponent<AudioSource>();

        SpriteRenderer background = GameObject.Find("Background").GetComponent<SpriteRenderer>();
        minX = background.bounds.min.x;
        maxX = background.bounds.max.x;
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
