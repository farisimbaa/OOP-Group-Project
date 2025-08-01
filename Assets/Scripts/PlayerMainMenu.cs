using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class PlayerMainMenu : MonoBehaviour
{
    public Sprite[] characterSprites;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        int selectedCharacterIndex = PlayerPrefs.GetInt("SelectedCharacter", 0);
        GetComponent<SpriteRenderer>().sprite = characterSprites[selectedCharacterIndex];
    }

    // Update is called once per frame
    void Update()
    {

    }
}
