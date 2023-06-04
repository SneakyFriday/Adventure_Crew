using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Story Character", menuName = "Character")]
public class SO_Character : ScriptableObject
{
    public enum CharacterClass
    {
        Paladin,
        Rogue,
        Warrior,
        Bard,
        Cleric,
        Druid,
        Ranger,
        Sorcerer,
        Warlock,
        Monk,
        Barbarian,
        Fighter,
        Wizard,
    }
    
    public enum CharacterMoods
    {
        Happy,
        Sad,
        Angry,
    }
    
    #region Variables

    public Sprite characterSprite;
    public Sprite[] characterMoodSprites;
    public Dictionary<CharacterMoods, Sprite> characterMoodSprite = new();
    [Header("Character Attributes")]
    public string characterName;
    public int characterAttentionValue;
    public CharacterClass characterClass;
    public PlaceIndexes currentCharacterPlace;
    public SO_Item[] desiredObjects;
    public Dictionary<PlaceIndexes, string[]> characterPlaceSpecificDialogue = new();

    #endregion

    #region Variables: Place Specific Dialogue
    
    [Header("Place Specific Dialogues")]
    [SerializeField] private string[] _characterDialogue_Tavern;
    [SerializeField] private string[] _characterDialogue_TownCenter;
    [SerializeField] private string[] _characterDialogue_Forest;
    [SerializeField] private string[] _characterDialogue_MagicShop;
    [SerializeField] private string[] _characterDialogue_Blacksmith;

    #endregion

    /**
     * Initialize the dictionary with the character's dialogue for each place.
     */
    public void Init()
    {
        // Reset Values
        characterAttentionValue = 0;

        // Attach Dialogue to Places in Dictionary
        characterPlaceSpecificDialogue.Add(PlaceIndexes.Tavern, _characterDialogue_Tavern);
        characterPlaceSpecificDialogue.Add(PlaceIndexes.TownCenter, _characterDialogue_TownCenter);
        characterPlaceSpecificDialogue.Add(PlaceIndexes.Forest, _characterDialogue_Forest);
        characterPlaceSpecificDialogue.Add(PlaceIndexes.MagicShop, _characterDialogue_MagicShop);
        characterPlaceSpecificDialogue.Add(PlaceIndexes.Blacksmith, _characterDialogue_Blacksmith);
        
        // Initialize the Mood Sprite Dictionary
        // TODO: This is a bit of a hack, but it works for now.
        // TODO: Find a better way to do this.
        characterMoodSprite.Add(CharacterMoods.Happy, characterMoodSprites[0]);
        characterMoodSprite.Add(CharacterMoods.Sad, characterMoodSprites[1]);
        characterMoodSprite.Add(CharacterMoods.Angry, characterMoodSprites[2]);
    }

    public string[] GetCharacterDialogue(PlaceIndexes place)
    {
        return characterPlaceSpecificDialogue[place];
    }
}

