using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.SocialPlatforms.Impl;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerLevel6 : Player
{
    public override void GameOver()
    {
        int final = ScoreSystemLevel6.Instance.GetScore();
        PlayerPrefs.SetInt("FinalScore", final);
        PlayerPrefs.Save();
        SceneManager.LoadScene("GameOver");
    }
}
