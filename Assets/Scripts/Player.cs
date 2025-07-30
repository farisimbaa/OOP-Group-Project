using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

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
    if (collision.gameObject.CompareTag("Obstacle"))
    {
        Debug.Log("Hit an obstacle. Game Over!");
        GameOver(); // Reuse your existing method
    }
    if (collision.gameObject.CompareTag("Platform"))
        {
            if (rb.linearVelocity.y <= 0f)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, moveSpeed);
            }
        }
}

// Called by spring

}