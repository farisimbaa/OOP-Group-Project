using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SocialPlatforms.Impl;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    public float moveSpeed = 10f;
    float movement = 0f;
    Rigidbody2D rb;
    public SpriteRenderer background;
    public Sprite[] characterSprites;
    private bool isGrounded;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public Image[] heartIcons; // Size 3 in Inspector
    public Sprite fullHeart;
    public Sprite emptyHeart;
    private bool isInvincible = false;
    public float invincibilityTime = 1f;
    public int lives = 3;
    public bool canJump = true;
    public float glideGravityScale = 0.5f;
    public float glideDuration = 5f;
    private bool isGliding = false;
    private float originalGravityScale;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        originalGravityScale = rb.gravityScale;
        int selectedCharacterIndex = PlayerPrefs.GetInt("SelectedCharacter", 0);
        GetComponent<SpriteRenderer>().sprite = characterSprites[selectedCharacterIndex];
    }

    // Update is called once per frame
    void Update()
    {
        movement = Input.GetAxis("Horizontal") * moveSpeed;

        if (movement > 0.1f)
            transform.localScale = new Vector3(1f, 1f, 1f);
        else if (movement < -0.1f)
            transform.localScale = new Vector3(-1f, 1f, 1f);

        Vector3 viewPos = Camera.main.WorldToViewportPoint(transform.position);
        if (viewPos.y < 0)
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
        WrapAroundByBackground();
    }

    void WrapAroundByBackground()
    {
        float minX = background.bounds.min.x;
        float maxX = background.bounds.max.x;
        Vector3 pos = transform.position;

        if (pos.x > maxX)
        {
            pos.x = minX;
        }
        else if (pos.x < minX)
        {
            pos.x = maxX;
        }

        transform.position = pos;
    }

    void UpdateHearts()
    {
        for (int i = 0; i < heartIcons.Length; i++)
        {
            if (i < lives)
            {
                heartIcons[i].sprite = fullHeart;
            }
            else
            {
                heartIcons[i].sprite = emptyHeart;
            }
        }
    }
    IEnumerator TemporaryInvincibility()
    {
        isInvincible = true;

        // Optional: change player color to flash red
        // GetComponent<SpriteRenderer>().color = Color.red;

        yield return new WaitForSeconds(invincibilityTime);

        // Reset
        // GetComponent<SpriteRenderer>().color = Color.white;
        isInvincible = false;
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
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
            StickyPlatform sticky = collision.gameObject.GetComponent<StickyPlatform>();
            canJump = (sticky == null); // Disable jump if sticky platform

            if (canJump && rb.linearVelocity.y <= 0f)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, moveSpeed);
            }
        }
     }
        void GameOver()
        {
            int final = ScoreSystem.Instance.GetScore();
            PlayerPrefs.SetInt("FinalScore", final);
            PlayerPrefs.Save();
            SceneManager.LoadScene("GameOver");
        }

    public void ActivateGlide()
    {
        if (!isGliding)
            StartCoroutine(GlideEffect());
    }

    IEnumerator GlideEffect()
    {
        isGliding = true;
        rb.gravityScale = glideGravityScale;

        float timer = 0f;
        while (timer < glideDuration)
        {
            // Cancel glide if grounded
            if (isGrounded)
            {
                break;
            }
            timer += Time.deltaTime;
            yield return null;
        }

        rb.gravityScale = originalGravityScale;
        isGliding = false;
    }
   
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("GlidePickup"))
        {
            Debug.Log("Glide pickup collected!");
            ActivateGlide();
            Destroy(other.gameObject);
        }
    }

}
