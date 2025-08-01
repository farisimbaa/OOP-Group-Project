using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManagerLevel6 : PauseManager
{
    void Start()
    {
        pauseMenuPanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public override void GiveUp()
    {
        Resume();
        PlayerPrefs.SetInt("FinalScore", ScoreSystemLevel6.Instance.GetScore());
        PlayerPrefs.Save();
        SceneManager.LoadScene("GameOver");
    }
}
