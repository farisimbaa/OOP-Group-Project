using System.Numerics;
using TMPro;
using UnityEngine;

public class ScoreSystem : MonoBehaviour
{
    public static ScoreSystem Instance;
    public Transform player;
    public TextMeshProUGUI scoreText;

    protected int highScore = 0;
    protected float highestY;
    protected int score = 0;
    protected int coinScore = 0;
    public AudioClip hundredEffectSound;
    public AudioClip thousandEffectSound;
    public AudioClip fireLoopSound;
    protected AudioSource audioSource;
    protected AudioSource fireLoopAudioSource;
    protected int lastHundredMilestone = 0;
    protected int lastThousandMilestone = 0;
    protected bool hundredEffect;
    protected bool thousandEffect;
    protected Color originalColor;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Start()
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
    public virtual void Update()
    {
        if (player.position.y > highestY)
        {
            highestY = player.position.y;
            UpdateScoreUI();
        }
    }

    public virtual void AddScore(int coinValue)
    {
        coinScore += coinValue;
        UpdateScoreUI();
    }

    public virtual void UpdateScoreUI()
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

    public System.Collections.IEnumerator PlayHundredEffect()
    {
        audioSource.PlayOneShot(hundredEffectSound);

        hundredEffect = true;

        scoreText.color = Color.red;

        yield return new WaitForSeconds(1.0f);

        scoreText.color = originalColor;
        hundredEffect = false;
    }

    public System.Collections.IEnumerator PlayThousandEffect()
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
