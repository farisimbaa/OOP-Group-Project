using UnityEngine;

public class Rocket : Pickup
{
    private float launchForce = 15f;
    public AudioClip rocketSound;

    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(playerTag))
        {
            Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                SoundManager.Instance.PlaySound(rocketSound);
                rb.AddForce(Vector2.up * launchForce * Platform.jumpForceMultiplier, ForceMode2D.Impulse);
            }
            Destroy(gameObject);
        }
    }
}