using UnityEngine;
using UnityEngine.SceneManagement;

public class Level5Manager : MonoBehaviour
{
    public float timeLimit = 60f;
    private float timer;
    public GameObject winPanel;
    public GameObject losePanel;

    void Start()
    {
        timer = timeLimit;
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            Lose();
        }
    }

    public void Win()
    {
        Time.timeScale = 0f;
        winPanel.SetActive(true);
    }

    public void Lose()
    {
        Time.timeScale = 0f;
        losePanel.SetActive(true);
    }

    public void Retry()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
