using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public GameObject pauseMenuPanel;

    void Start()
    {
        pauseMenuPanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void OpenPauseMenu()
    {
        pauseMenuPanel.SetActive(true);
        Pause();
    }

    public void Menu()
    { 
        if (pauseMenuPanel.activeSelf)
        {
            ClosePauseMenu();
        }
        else
        {
            OpenPauseMenu();
        }
    }

    public void ClosePauseMenu()
    {
        pauseMenuPanel.SetActive(false);
        Resume();
    }

    public void Resume()
    {
        Time.timeScale = 1f;
    }

    public void Pause()
    {
        Time.timeScale = 0f;
    }

    public virtual void GiveUp()
    {
        Resume();
        PlayerPrefs.SetInt("FinalScore", ScoreSystem.Instance.GetScore());
        PlayerPrefs.Save();
        SceneManager.LoadScene("GameOver");
    }
}
