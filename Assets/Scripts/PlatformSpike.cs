using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlatformSpike : Platform
{
    public AudioClip spikeSound;

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
        SoundManager.Instance.PlaySound(spikeSound);
        yield return new WaitForSeconds(spikeSound.length);
        int final = ScoreSystem.Instance.GetScore();
        PlayerPrefs.SetInt("FinalScore", final);
        PlayerPrefs.Save();
        SceneManager.LoadScene("GameOver");
    }
}