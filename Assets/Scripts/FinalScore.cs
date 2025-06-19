using UnityEngine;
using TMPro;

public class FinalScore : MonoBehaviour
{
    public TextMeshProUGUI finalScoreText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        int finalScore = PlayerPrefs.GetInt("FinalScore");
        finalScoreText.text = "Final Score: " + finalScore.ToString();
    }
}
