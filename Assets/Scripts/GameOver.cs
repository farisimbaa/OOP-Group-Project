using UnityEngine;
using TMPro;

public class GameOver : MonoBehaviour
{
    public TextMeshProUGUI gameOverText;
    void Start()
    {
        gameOverText.text = "Game Over!\n";
    }
}
