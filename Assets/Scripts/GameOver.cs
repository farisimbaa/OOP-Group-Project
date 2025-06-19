using UnityEngine;
using TMPro;

public class GameOver : MonoBehaviour
{
    public TextMeshProUGUI gameOverText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameOverText.text = "Game Over!\n";
    }
}
