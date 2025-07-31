using UnityEngine;
using UnityEngine.SceneManagement;

public class Spikes : MonoBehaviour
{
    public string playerTag = "Player";
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.relativeVelocity.y <= 0f)
        {

            if (collision.collider.CompareTag(playerTag))
            {
                Debug.Log("Player hit the spikes!");
                int final = ScoreSystem.Instance.GetScore();
                PlayerPrefs.SetInt("FinalScore", final);
                PlayerPrefs.Save();

                SceneManager.LoadScene("GameOver"); // Load your Game Over screen
            }
        }
    }
}
