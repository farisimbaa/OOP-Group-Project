using UnityEngine;
using UnityEngine.UI;

public class Level3Manager : MonoBehaviour
{
    public int requiredItems = 5;
    public float levelTime = 60f;
    // public Text timerText;
    // public GameObject exitPortal;

    private float timeLeft;
    private int collectedItems = 0;
    private bool levelEnded = false;

    void Start()
    {
        timeLeft = levelTime;
        // exitPortal.SetActive(false);
    }

    void Update()
    {
        if (levelEnded) return;

        timeLeft -= Time.deltaTime;
        // timerText.text = "Time: " + Mathf.CeilToInt(timeLeft).ToString();

        if (timeLeft <= 0)
            LoseGame();
    }

    public void CollectItem()
    {
        collectedItems++;
        if (collectedItems >= requiredItems) { }
            // exitPortal.SetActive(true);
    }

    public void WinGame()
    {
        levelEnded = true;
        Debug.Log("YOU WIN!");
    }

    public void LoseGame()
    {
        levelEnded = true;
        Debug.Log("YOU LOSE!");
    }
}

