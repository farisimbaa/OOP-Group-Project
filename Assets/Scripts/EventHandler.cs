using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public AudioClip buttonSound;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayGame()
    {
        audioSource.PlayOneShot(buttonSound);
        SceneManager.LoadScene("Level1");
    }

    public void QuitGame()
    {
        audioSource.PlayOneShot(buttonSound);
        Application.Quit();
    }

    public void LoadMainMenu()
    {
        audioSource.PlayOneShot(buttonSound);
        SceneManager.LoadScene("MainMenu");
    }

    public void Nothing()
    {
        audioSource.PlayOneShot(buttonSound);
        // This function intentionally does nothing
    }

    public void PlayButtonSound()
    {
        audioSource.PlayOneShot(buttonSound);
    }
}
