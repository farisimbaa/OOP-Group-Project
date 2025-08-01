using UnityEngine;
using UnityEngine.SceneManagement;

public class KillerMonster : MonoBehaviour
{
    public AudioClip hitSound;
    public float fallDelay = 1.5f;  // Delay before Game Over

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Debug.Log("Player hit killer monster!");

            if (hitSound)
                AudioSource.PlayClipAtPoint(hitSound, transform.position);

            // Disable player movement (assumes a PlayerMovement script)
            MonoBehaviour playerScript = collision.collider.GetComponent<MonoBehaviour>();
            if (playerScript != null) playerScript.enabled = false;

            // Allow the player to fall (disable constraints)
            Rigidbody2D rb = collision.collider.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.linearVelocity = Vector2.zero;
                rb.constraints = RigidbodyConstraints2D.None;
                rb.gravityScale = 3f; // You can tweak the gravity to fall faster
            }

            // Start Game Over after a delay
            StartCoroutine(DelayedGameOver());
        }
    }

    private System.Collections.IEnumerator DelayedGameOver()
    {
        int finalScore = ScoreSystem.Instance.GetScore();
        PlayerPrefs.SetInt("FinalScore", finalScore);
        PlayerPrefs.Save();

        yield return new WaitForSeconds(fallDelay);
        SceneManager.LoadScene("GameOver");
    }
}
