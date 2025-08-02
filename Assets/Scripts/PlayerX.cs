using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SocialPlatforms.Impl;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerX : MonoBehaviour
{
    public float moveSpeed = 10f;
    float movement = 0f;
    Rigidbody2D rb;
    public SpriteRenderer background;
    public Sprite[] characterSprites;
    private bool isGrounded;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public bool canJump = true;
    public float glideGravityScale = 0.5f;
    public float glideDuration = 5f;
    private bool isGliding = false;
    private float originalGravityScale;
    private bool canDoubleJump = false;
    public float doubleJumpForce = 8f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        originalGravityScale = rb.gravityScale;
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
        if (!isGrounded && canDoubleJump && rb.linearVelocity.y < 0)
        {
            ActivateDoubleJump();
        }
    }
    void CheckGrounded()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);

        if (isGrounded)
        {
            canDoubleJump = true;
        }

    }
    void FixedUpdate()
    {
        Vector2 velocity = rb.linearVelocity;
        velocity.x = movement;
        rb.linearVelocity = velocity;

        CheckGrounded();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
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
        else if (other.CompareTag("DoubleJumpPickup"))
        {
            Debug.Log("Double jump pickup collected!");
            ActivateDoubleJump();
            Destroy(other.gameObject);
        }
    }

    public void ActivateDoubleJump()
    {
        if (!isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, doubleJumpForce);
            canDoubleJump = false; // Disable after use
        }
        else
        {
            canDoubleJump = true; // Allow 1 double jump if collected while grounded
        }
    }
}