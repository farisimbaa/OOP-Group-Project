using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerLevel2 : MonoBehaviour
{
    public float moveSpeed = 5f;
    protected float movement;
    protected Rigidbody2D rb;
    public SpriteRenderer background;
    public Sprite[] characterSprites;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        int selectedCharacterIndex = PlayerPrefs.GetInt("SelectedCharacter", 0);
        GetComponent<SpriteRenderer>().sprite = characterSprites[selectedCharacterIndex];
    }

    // Update is called once per frame
    public void Update()
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

    public void FixedUpdate()
    {
        Vector2 velocity = rb.linearVelocity;
        velocity.x = movement;
        rb.linearVelocity = velocity;
    }

    public void LateUpdate()
    {
        WrapAroundByBackground();
    }

    public void WrapAroundByBackground()
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

    public virtual void GameOver()
    {
        int final = ScoreSystem.Instance.GetScore();
        PlayerPrefs.SetInt("FinalScore", final);
        PlayerPrefs.Save();
        SceneManager.LoadScene("GameOver");
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