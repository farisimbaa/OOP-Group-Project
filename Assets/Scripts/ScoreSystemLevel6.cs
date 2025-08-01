using System.Numerics;
using TMPro;
using UnityEngine;

public class ScoreSystemLevel6 : ScoreSystem
{
    public TextMeshProUGUI multiplierText;
    public new static ScoreSystemLevel6 Instance;
    protected float verticalRawScore = 0f;
    protected float coinRawScore = 0f;

    protected float scoreMultiplier = 1f;

    public override void Update()
    {
        if (player.position.y > highestY)
        {
            float deltaY = player.position.y - highestY;
            highestY = player.position.y;
            verticalRawScore += deltaY * scoreMultiplier;
            UpdateScoreUI();
        }
    }

    private void Awake()
    { 
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public override void AddScore(int coinValue)
    {
        coinRawScore += coinValue * scoreMultiplier;
        UpdateScoreUI();
    }

    public override void UpdateScoreUI()
    {
        score = Mathf.RoundToInt(verticalRawScore + coinRawScore);

        scoreText.text = "Score: " + score.ToString();
        multiplierText.text = "Multiplier: x" + scoreMultiplier.ToString("0.00");

        int hundredMilestone = score / 100;
        int thousandMilestone = score / 1000;

        if (lastHundredMilestone < hundredMilestone && !hundredEffect)
        {
            lastHundredMilestone = hundredMilestone;
            StartCoroutine(PlayHundredEffect());
        }

        if (lastThousandMilestone < thousandMilestone && !thousandEffect)
        {
            lastThousandMilestone = thousandMilestone;
            StartCoroutine(PlayThousandEffect());
        }

        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore);
            PlayerPrefs.Save();
        }
    }

    public void ApplyScoreMultiplier(float multiplier)
    {
        scoreMultiplier *= multiplier;
    }

    public void IncreaseMultiplier(int amount)
    {
        scoreMultiplier += amount;

    }
}