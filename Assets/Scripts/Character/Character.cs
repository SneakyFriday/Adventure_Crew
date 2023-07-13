using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour, ICharacter
{
    [SerializeField] private SO_Character[] characterData;

    [Header("Character Properties")] 
    [SerializeField] private TextMeshProUGUI characterName;
    [SerializeField] private Image characterImage;
    [SerializeField] private Image characterAttentionBarFill;
    
    [Header("Special Mood Sprites")]
    [SerializeField] private Image characterMoodImage;
    private Sprite[] _characterMoodSprites;
    
    [SerializeField] private PlaceManager _placeManager;
    
    private int _characterAttentionValue;
    private SO_Character _currentCharacterData;
    private SO_Character.CharacterClass _characterClass;
    private SO_Item[] _desiredObjects;

    public event Action<SO_Character, PlaceIndexes> onCharacterDialogue;

    private void Start()
    {
        _placeManager.onPlaceChanged.AddListener(InitCharacter);
        foreach (var character in characterData)
        {
            character.Init();
        }
    }

    /**
     * Check if the character is at the current location.
     * @return SO_Character
     */
    private SO_Character CheckIfCharacterIsAtLocation()
    {
        // Loops through the characterData array and checks if the current character is at the current location
        foreach (var character in characterData)
        {
            if (character.currentCharacterPlace != _placeManager.GetCurrentPlace()) continue;
            return character;
        }
        print("No Character at this location! " + _placeManager.GetCurrentPlace());
        return null;
    }

    /**
     * Initialize the character with the data from the SO_Character.
     * @return void
     */
    private void InitCharacter()
    {
        _currentCharacterData = CheckIfCharacterIsAtLocation();
        
        if(_currentCharacterData == null)
        {
            // Set Character Properties to null when there is no character at the location
            characterName.text = " ";
            _desiredObjects = null;
            characterAttentionBarFill.fillAmount = 0f;
            
            // Hide the character image and mood image
            DeactivateCharacter();
            
            return;
        }
        
        // Show the character image and mood image
        characterImage.gameObject.SetActive(true);
        characterMoodImage.gameObject.SetActive(true);
        
        // Set Character Properties
        characterImage.sprite = _currentCharacterData.characterSprite;
        characterName.text = _currentCharacterData.characterName;
        _characterAttentionValue = _currentCharacterData.characterAttentionValue;
        _characterClass = _currentCharacterData.characterClass;
        _desiredObjects = _currentCharacterData.desiredObjects;
        characterAttentionBarFill.fillAmount = _characterAttentionValue / 100f;
        
        // Set Neutral/Happy Mood Sprite for the Character for Start
        // TODO: Set the mood based on the character's mood
        characterMoodImage.sprite = _currentCharacterData.characterMoodSprite[0];
        
        // Set the character's dialogue for the current location
        onCharacterDialogue?.Invoke(_currentCharacterData, _placeManager.GetCurrentPlace());
    }

    public void ChangeAttentionValue(int value)
    {
        // Save the new attention value to the SO_Character
        _currentCharacterData.characterAttentionValue += value;
        
        // Update the attention value
        _characterAttentionValue = _currentCharacterData.characterAttentionValue;
        // print("Character Attention Value: " + _characterAttentionValue);
        characterAttentionBarFill.fillAmount = _characterAttentionValue / 100f;
    }

    public SO_Item[] GetDesiredObjects()
    {
        return _desiredObjects;
    }

    public SO_Character.CharacterClass GetCharacterClass()
    {
        return _characterClass;
    }
    
    public void SetCharacterMood(SO_Character.CharacterMoods mood)
    {
        characterMoodImage.sprite = characterData[0].characterMoodSprite[mood];
    }
    
    public SO_Character[] GetCharacterData()
    {
        return characterData;
    }

    public void DeactivateCharacter()
    {
        // Hide the character image and mood image
        // TODO: Hide the character image and mood image with Blend
        characterImage.gameObject.SetActive(false);
        characterMoodImage.gameObject.SetActive(false);
    }
}