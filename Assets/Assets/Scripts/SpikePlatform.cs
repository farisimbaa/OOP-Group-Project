using UnityEngine;
using UnityEngine.SceneManagement;

public class SpikePlatform : Platform
{
    public AudioClip deathSound;
    public GameObject deathEffect;


    public override void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            if (deathSound)
                AudioSource.PlayClipAtPoint(deathSound, transform.position);

            if (deathEffect)
                Instantiate(deathEffect, collision.transform.position, Quaternion.identity);

            Scene currentScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(currentScene.name);
        }
    }
}