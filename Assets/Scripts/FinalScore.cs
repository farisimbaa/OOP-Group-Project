using UnityEngine;
using TMPro;

public class FinalScore : MonoBehaviour
{
    public TextMeshProUGUI finalScoreText;
    public TextMeshProUGUI highScoreText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        int highScore = PlayerPrefs.GetInt("HighScore");
        int finalScore = PlayerPrefs.GetInt("FinalScore");
        finalScoreText.text = "Final Score: " + finalScore.ToString();
        highScoreText.text = "Highest Score: " + highScore.ToString();
    }
}
