using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTracker : MonoBehaviour
{
    void Start()
    {
        PlayerPrefs.SetString("LastPlayedLevel", SceneManager.GetActiveScene().name);
        PlayerPrefs.Save();
    }
}
