using UnityEngine;

public class CharacterSelectionManager : MonoBehaviour
{
    public GameObject characterSelectionPanel;
    public Sprite[] characterSprites;
    public SpriteRenderer playerSprite;
    private int selectedCharacterIndex = 0;

    void Start()
    {
        characterSelectionPanel.SetActive(false);
        UpdatePlayerSprite();
    }

    public void OpenCharacterSelection()
    { 
        characterSelectionPanel.SetActive(true);
    }

    public void CloseCharacterSelection()
    {
        characterSelectionPanel.SetActive(false);
    }

    public void SelectCharacter(int index)
    {
        selectedCharacterIndex = index;
        PlayerPrefs.SetInt("SelectedCharacter", selectedCharacterIndex);
        PlayerPrefs.Save();
        UpdatePlayerSprite();
    }

    void UpdatePlayerSprite()
    {
        playerSprite.sprite = characterSprites[selectedCharacterIndex];
    }
}
