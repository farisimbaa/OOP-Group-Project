using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlatformSpike : Platform
{
    public AudioClip spikeSound;

    public override void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public override void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.relativeVelocity.y <= 0f)
        {
            {
                StartCoroutine(SpikeEffect());
            }
        }
    }

    private IEnumerator SpikeEffect()
    {
        audioSource.PlayOneShot(spikeSound);
        yield return new WaitForSeconds(spikeSound.length);
        SceneManager.LoadScene("GameOver");
    }
}