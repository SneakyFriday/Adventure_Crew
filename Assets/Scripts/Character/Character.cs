using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour, ICharacter
{
    [SerializeField] private SO_Character[] characterData;
    [SerializeField] private TextMeshProUGUI characterName;
    [SerializeField] private Image characterImage;
    [SerializeField] private Image characterAttentionBarFill;
    [SerializeField] private Image characterMoodImage;
    private Sprite[] _characterMoodSprites;

    [SerializeField] private PlaceManager _placeManager;

    private int _characterAttentionValue;
    private SO_Character _currentCharacterData;
    private SO_Character.CharacterClass _characterClass;
    private SO_Item[] _desiredObjects;

    public event Action<SO_Character, PlaceIndexes> onCharacterDialogue;

    private void Awake()
    {
        _placeManager.onPlaceChanged.AddListener(InitCharacter);
    }

    private void Start()
    {
        InitAllCharacters();
    }

    private void InitAllCharacters()
    {
        foreach (var character in characterData)
        {
            character.Init();
        }
    }

    private SO_Character CheckIfCharacterIsAtLocation()
    {
        foreach (var character in characterData)
        {
            if (character.currentCharacterPlace == _placeManager.GetCurrentPlace())
            {
                return character;
            }
        }

        Debug.Log("No Character at this location! " + _placeManager.GetCurrentPlace());
        return null;
    }

    private void InitCharacter()
    {
        _currentCharacterData = CheckIfCharacterIsAtLocation();

        if (_currentCharacterData == null)
        {
            DeactivateCharacterUI();
            return;
        }

        ActivateCharacterUI();
        SetCharacterProperties();
        SetCharacterMoodSprite();

        onCharacterDialogue?.Invoke(_currentCharacterData, _placeManager.GetCurrentPlace());
    }

    private void ActivateCharacterUI()
    {
        characterImage.gameObject.SetActive(true);
        characterMoodImage.gameObject.SetActive(true);
    }
    
    public void DeactivateCharacterUI()
    {
        characterImage.gameObject.SetActive(false);
        characterMoodImage.gameObject.SetActive(false);
    }

    private void SetCharacterProperties()
    {
        characterImage.sprite = _currentCharacterData.characterSprite;
        characterName.text = _currentCharacterData.characterName;
        _characterAttentionValue = _currentCharacterData.characterAttentionValue;
        _characterClass = _currentCharacterData.characterClass;
        _desiredObjects = _currentCharacterData.desiredObjects;
        characterAttentionBarFill.fillAmount = _characterAttentionValue / 100f;
    }

    private void SetCharacterMoodSprite()
    {
        characterMoodImage.sprite = _currentCharacterData.characterMoodSprite[0];
    }

    public void ChangeAttentionValue(int value)
    {
        _currentCharacterData.characterAttentionValue += value;
        _characterAttentionValue = _currentCharacterData.characterAttentionValue;
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
        characterMoodImage.sprite = _currentCharacterData.characterMoodSprite[mood];
    }

    public SO_Character[] GetCharacterData()
    {
        return characterData;
    }
}