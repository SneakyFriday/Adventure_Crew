using System;
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
    public Dictionary<PlaceIndexes, string[]> characterPlaceSpecificDialogueAnswers = new();

    #endregion

    #region Variables: Place Specific Dialogue
    
    [Header("Place Specific Dialogues")]
    [TextArea(5, 10)]
    [SerializeField] private string[] _characterDialogue_Tavern;
    [TextArea(1, 2)]
    [SerializeField] private string[] _characterDialogue_Tavern_answers;
    [TextArea(5, 10)]
    [SerializeField] private string[] _characterDialogue_TownCenter;
    [TextArea(1, 2)]
    [SerializeField] private string[] _characterDialogue_TownCenter_answers;
    [TextArea(5, 10)]
    [SerializeField] private string[] _characterDialogue_Forest;
    [TextArea(1, 2)]
    [SerializeField] private string[] _characterDialogue_Forest_answers;
    [TextArea(5, 10)]
    [SerializeField] private string[] _characterDialogue_MagicShop;
    [TextArea(1, 2)]
    [SerializeField] private string[] _characterDialogue_MagicShop_answers;
    [TextArea(5, 10)]
    [SerializeField] private string[] _characterDialogue_Blacksmith;
    [TextArea(1, 2)]
    [SerializeField] private string[] _characterDialogue_Blacksmith_answers;

    #endregion

    /**
     * Initialize the dictionary with the character's dialogue for each place.
     */
    public void Init()
    {
        // Reset Values
        characterAttentionValue = 0;

        // TODO: Use a loop to do this instead of hardcoding it.
        // TODO: Use tuples.
        // Attach Dialogue to Places in Dictionary
        characterPlaceSpecificDialogue.Add(PlaceIndexes.Tavern, _characterDialogue_Tavern);
        characterPlaceSpecificDialogue.Add(PlaceIndexes.TownCenter, _characterDialogue_TownCenter);
        characterPlaceSpecificDialogue.Add(PlaceIndexes.Forest, _characterDialogue_Forest);
        characterPlaceSpecificDialogue.Add(PlaceIndexes.MagicShop, _characterDialogue_MagicShop);
        characterPlaceSpecificDialogue.Add(PlaceIndexes.Blacksmith, _characterDialogue_Blacksmith);
        
        // Attach Dialogue Answers to Places in Dictionary
        characterPlaceSpecificDialogueAnswers.Add(PlaceIndexes.Tavern, _characterDialogue_Tavern_answers);
        characterPlaceSpecificDialogueAnswers.Add(PlaceIndexes.TownCenter, _characterDialogue_TownCenter_answers);
        characterPlaceSpecificDialogueAnswers.Add(PlaceIndexes.Forest, _characterDialogue_Forest_answers);
        characterPlaceSpecificDialogueAnswers.Add(PlaceIndexes.MagicShop, _characterDialogue_MagicShop_answers);
        characterPlaceSpecificDialogueAnswers.Add(PlaceIndexes.Blacksmith, _characterDialogue_Blacksmith_answers);
        
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

