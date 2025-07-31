using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManagerLevel6 : MonoBehaviour
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

    public void GiveUp()
    {
        Resume();
        PlayerPrefs.SetInt("FinalScore", ScoreSystemLevel6.Instance.GetScore());
        PlayerPrefs.Save();
        SceneManager.LoadScene("GameOver");
    }
}
