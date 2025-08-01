using UnityEngine;
using System.Collections;

public class PowerupManager : MonoBehaviour
{
    public AudioClip powerUpSound;
    public AudioClip powerDownSound;
    public static PowerupManager Instance;
    private Coroutine jumpBoostCoroutine;
    private float jumpBoostEndTime;
    private Coroutine speedBoostCoroutine;
    private float speedBoostEndTime;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Platform.jumpForceMultiplier = 1f;
    }

    public void StartJumpBoost(float duration)
    {
        jumpBoostEndTime = Time.time + duration;
        if (jumpBoostCoroutine == null)
        {
            jumpBoostCoroutine = StartCoroutine(JumpBoostRoutine(duration));
        }
        else SoundManager.Instance.PlaySound(powerUpSound);
        
    }

    private IEnumerator JumpBoostRoutine(float duration)
    {
        SoundManager.Instance.PlaySound(powerUpSound);
        Platform.jumpForceMultiplier = 1.5f;
        while (Time.time < jumpBoostEndTime)
        {
            yield return null;
        }
        Platform.jumpForceMultiplier = 1f;
        SoundManager.Instance.PlaySound(powerDownSound);
        jumpBoostCoroutine = null;
    }

    public void StartSpeedBoost(float duration, PlayerLevel6 player)
    {
        speedBoostEndTime = Time.time + duration;
        if (speedBoostCoroutine == null)
        {
            speedBoostCoroutine = StartCoroutine(SpeedBoostRoutine(duration, player));
        }
        else SoundManager.Instance.PlaySound(powerUpSound);

    }

    private IEnumerator SpeedBoostRoutine(float duration, PlayerLevel6 player)
    {
        SoundManager.Instance.PlaySound(powerUpSound);
        player.moveSpeed = 10f;

        while (Time.time < speedBoostEndTime)
        {
            yield return null;
        }

        player.moveSpeed = 5f;
        SoundManager.Instance.PlaySound(powerDownSound);
        speedBoostCoroutine = null;
    }
}
