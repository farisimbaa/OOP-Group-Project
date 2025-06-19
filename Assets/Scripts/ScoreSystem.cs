using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ScoreSystem : MonoBehaviour
{
    public Transform player;
    public TextMeshProUGUI scoreText;

    private float highestY;
    private int score = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        highestY = player.position.y;
        UpdateScoreUI();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.position.y > highestY)
        {
            highestY = player.position.y;
            score = Mathf.FloorToInt(highestY);
            UpdateScoreUI();
        }
    }

    void UpdateScoreUI()
    {
        scoreText.text = "Score: " + score.ToString();
    }

    public int GetScore()
    {
        return score;
    }
}
