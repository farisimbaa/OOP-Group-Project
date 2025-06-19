using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TimeAttack : MonoBehaviour
{

    public int timeLimit;
    public TextMeshProUGUI timerText;
    private float currentTime;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentTime = timeLimit;
        UpdateTimerUI();
    }

    // Update is called once per frame
    void Update()
    {
        currentTime -= Time.deltaTime;
        if (currentTime <= 0)
        {
            currentTime = 0;
            SceneManager.LoadScene("GameOver");
        }
        UpdateTimerUI();
    }

    void UpdateTimerUI()
    {
        timerText.text = "Time: " + Mathf.CeilToInt(currentTime).ToString();
    }
}
