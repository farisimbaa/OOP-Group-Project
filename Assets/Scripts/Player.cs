using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.SocialPlatforms.Impl;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    public float moveSpeed = 10f;
    float movement = 0f;
    Rigidbody2D rb;
    public SpriteRenderer background;
    public Sprite[] characterSprites;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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

    void GameOver()
    {

        int final = ScoreSystem.Instance.GetScore();
         Debug.Log("Final Score: " + final);
        PlayerPrefs.SetInt("FinalScore", ScoreSystem.Instance.GetScore());
        PlayerPrefs.Save();
        SceneManager.LoadScene("GameOver");
}
}
