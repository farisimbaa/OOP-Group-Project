using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    public float moveSpeed = 10f;
    float movement = 0f;
    Rigidbody2D rb;
    public SpriteRenderer background;
    private bool isGrounded;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public Image[] heartIcons; // Size 3 in Inspector
    public Sprite fullHeart;
    public Sprite emptyHeart;
    private bool isInvincible = false;
    public float invincibilityTime = 1f;
     public int lives = 3;

   

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
        
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

    IEnumerator GameOverDelay()
{
    yield return new WaitForSeconds(1f); // Wait 1 second
    SceneManager.LoadScene("GameOver");
}

void GameOver()
{
    StartCoroutine(GameOverDelay());
}

void OnCollisionEnter2D(Collision2D collision)
{
    if (collision.gameObject.CompareTag("Obstacle") && !isInvincible)
    {
        lives--;

        UpdateHearts(); // 👈 Add this line here

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

}