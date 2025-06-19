using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ScoreSystem : MonoBehaviour
{
    public static ScoreSystem Instance;
    public Transform player;
    public TextMeshProUGUI scoreText;

    private float highestY;
    private int score = 0;
    private int coinScore = 0;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject); // Ensure only one instance exists
    }

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
            UpdateScoreUI();
        }
    }

    public void AddScore(int value)
    {
        coinScore += value;
        UpdateScoreUI();
    }

    void UpdateScoreUI()
    {
        score = Mathf.FloorToInt(highestY) + coinScore;
        scoreText.text = "Score: " + score.ToString();
    }

    public int GetScore()
    {
        return score;
    }
}
