using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerLevel2 : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 10f;
    private float movement;
    private Rigidbody2D rb;

    [Header("Background")]
    public SpriteRenderer background;

    [Header("Hearts UI")]
    public Image[] heartIcons;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    [Header("Lives")]
    public int lives = 3;
    private bool isInvincible = false;
    public float invincibilityTime = 1f;

    [Header("Character Sprites")]
    public Sprite[] characterSprites;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Set character sprite from PlayerPrefs
        int selectedCharacterIndex = PlayerPrefs.GetInt("SelectedCharacter", 0);
        if (selectedCharacterIndex < characterSprites.Length)
        {
            GetComponent<SpriteRenderer>().sprite = characterSprites[selectedCharacterIndex];
        }
    }

    void Update()
    {
        movement = Input.GetAxis("Horizontal") * moveSpeed;

        // Flip sprite based on direction
        if (movement > 0.1f)
            transform.localScale = new Vector3(1f, 1f, 1f);
        else if (movement < -0.1f)
            transform.localScale = new Vector3(-1f, 1f, 1f);

        // Check if player fell off screen
        if (Camera.main.WorldToViewportPoint(transform.position).y < 0)
        {
            GameOver();
        }
    }

    void FixedUpdate()
    {
        Vector2 velocity = rb.linearVelocity;
        velocity.x = movement;
        rb.linearVelocity = velocity;
    }

    void LateUpdate()
    {
        WrapAroundBackground();
    }

    void WrapAroundBackground()
    {
        if (background == null) return;

        float minX = background.bounds.min.x;
        float maxX = background.bounds.max.x;

        Vector3 pos = transform.position;
        if (pos.x > maxX) pos.x = minX;
        else if (pos.x < minX) pos.x = maxX;

        transform.position = pos;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Monster"))
        {
            GameOver();
        }

        if (collision.gameObject.CompareTag("Obstacle") && !isInvincible)
        {
            lives--;
            UpdateHearts();

            if (lives <= 0)
            {
                GameOver();
            }
            else
            {
                StartCoroutine(TemporaryInvincibility());
            }
        }

        if (collision.gameObject.CompareTag("Platform"))
        {
            if (rb.linearVelocity.y <= 0f)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, moveSpeed);
            }
        }
    }

    void UpdateHearts()
    {
        for (int i = 0; i < heartIcons.Length; i++)
        {
            heartIcons[i].sprite = i < lives ? fullHeart : emptyHeart;
        }
    }

    IEnumerator TemporaryInvincibility()
    {
        isInvincible = true;
        yield return new WaitForSeconds(invincibilityTime);
        isInvincible = false;
    }

    void GameOver()
    {
        int final = 0;

        if (ScoreSystem.Instance != null)
        {
            final = ScoreSystem.Instance.GetScore();
        }

        PlayerPrefs.SetInt("FinalScore", final);
        PlayerPrefs.Save();
        SceneManager.LoadScene("GameOver");
    }
}