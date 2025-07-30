using System.Numerics;
using TMPro;
using UnityEngine;

public class ScoreSystem : MonoBehaviour
{
    public static ScoreSystem Instance;
    public Transform player;
    public TextMeshProUGUI scoreText;

    private int highScore = 0;
    private float highestY;
    private int score = 0;
    private int coinScore = 0;
    public AudioClip hundredEffectSound;
    public AudioClip thousandEffectSound;
    public AudioClip fireLoopSound;
    private AudioSource audioSource;
    private AudioSource fireLoopAudioSource;
    private int lastHundredMilestone = 0;
    private int lastThousandMilestone = 0;
    private bool hundredEffect;
    private bool thousandEffect;
    private Color originalColor;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        highScore = PlayerPrefs.GetInt("HighScore", 0);

        hundredEffect = false;
        originalColor = scoreText.color;
        audioSource = GetComponent<AudioSource>();
        fireLoopAudioSource = gameObject.AddComponent<AudioSource>();
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

    public void AddScore(int coinValue)
    {
        coinScore += coinValue;
        UpdateScoreUI();
    }

    void UpdateScoreUI()
    {
        score = Mathf.FloorToInt(highestY) + coinScore;
        scoreText.text = "Score: " + score.ToString();

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

    public int GetScore()
    {
        return score;
    }

    private System.Collections.IEnumerator PlayHundredEffect()
    {
        audioSource.PlayOneShot(hundredEffectSound);

        hundredEffect = true;

        scoreText.color = Color.red;

        yield return new WaitForSeconds(1.0f);

        scoreText.color = originalColor;
        hundredEffect = false;
    }

    private System.Collections.IEnumerator PlayThousandEffect()
    {
        fireLoopAudioSource.clip = fireLoopSound;
        fireLoopAudioSource.loop = true;
        fireLoopAudioSource.Play();
        audioSource.PlayOneShot(thousandEffectSound);
        
        thousandEffect = true;

        scoreText.color = Color.red;

        UnityEngine.Vector3 originalScale = scoreText.transform.localScale;
        scoreText.transform.localScale *= 2.0f;

        yield return new WaitForSeconds(1.0f);

        scoreText.transform.localScale = originalScale;
        scoreText.color = originalColor;
        thousandEffect = false;
    }
}
