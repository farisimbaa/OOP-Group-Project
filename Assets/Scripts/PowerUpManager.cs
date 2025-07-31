using UnityEngine;
using System.Collections;

public class PowerupManager : MonoBehaviour
{
    public AudioClip powerUpSound;
    public AudioClip powerDownSound;
    public static PowerupManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void StartJumpBoost(float duration)
    {
        StartCoroutine(JumpBoostRoutine(duration));
    }

    private IEnumerator JumpBoostRoutine(float duration)
    {
        SoundManager.Instance.PlaySound(powerUpSound);
        Platform.jumpForceMultiplier = 1.5f;
        yield return new WaitForSeconds(duration);
        Platform.jumpForceMultiplier = 1f;
        SoundManager.Instance.PlaySound(powerDownSound);
    }

    public void StartSpeedBoost(float duration, PlayerLevel6 player)
    {
        StartCoroutine(SpeedBoostRoutine(duration, player));
    }

    private IEnumerator SpeedBoostRoutine(float duration, PlayerLevel6 player)
    {
        SoundManager.Instance.PlaySound(powerUpSound);
        float originalSpeed = player.moveSpeed;
        player.moveSpeed *= 2f;
        yield return new WaitForSeconds(duration);
        player.moveSpeed = originalSpeed;
        SoundManager.Instance.PlaySound(powerDownSound);
    }
}
