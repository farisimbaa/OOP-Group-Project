using UnityEngine;

public class FeatherJump : MonoBehaviour
{
    public AudioClip pickupSound;
    public GameObject pickupEffect;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (pickupSound)
                AudioSource.PlayClipAtPoint(pickupSound, transform.position);

            if (pickupEffect)
                Instantiate(pickupEffect, transform.position, Quaternion.identity);

            // Level3Manager.Instance.AddFeather();

            Destroy(gameObject);
        }
    }
}