using System;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

[CreateAssetMenu(fileName = "New Story Character", menuName = "Character")]
public class SO_Character : ScriptableObject
{
    private class DialogueData
    {
        public string[] Dialogue;
        public string[] Answers;
    }
    
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
    
    // TODO: Testing ... Question + Answers
    public Dictionary<string, string[]> characterGenericDialogueAndAnswers = new();

    #endregion

    #region Variables: Place Specific Dialogue
    
    [Header("Place Specific Dialogues")]
    [TextArea(5, 10)] [SerializeField] private string[] _characterDialogue_Tavern;
    [TextArea(1, 2)] [SerializeField] private string[] _characterDialogue_Tavern_answers;
    [TextArea(5, 10)] [SerializeField] private string[] _characterDialogue_TownCenter;
    [TextArea(1, 2)] [SerializeField] private string[] _characterDialogue_TownCenter_answers;
    [TextArea(5, 10)] [SerializeField] private string[] _characterDialogue_Forest;
    [TextArea(1, 2)] [SerializeField] private string[] _characterDialogue_Forest_answers;
    [TextArea(5, 10)] [SerializeField] private string[] _characterDialogue_MagicShop;
    [TextArea(1, 2)] [SerializeField] private string[] _characterDialogue_MagicShop_answers;
    [TextArea(5, 10)] [SerializeField] private string[] _characterDialogue_Blacksmith;
    [TextArea(1, 2)] [SerializeField] private string[] _characterDialogue_Blacksmith_answers;
    
    [Header("Generic Dialogue for Testing Purposes")]
    [TextArea(5, 10)] [SerializeField] private string _characterDialogue_firstMet = "";
    [TextArea(1, 2)] [SerializeField] private string[] _characterDialogue_firstMet_answers;
    [TextArea(5, 10)] [SerializeField] private string _characterDialogue_interceptMet = "";
    [TextArea(1, 2)] [SerializeField] private string[] _characterDialogue_interceptMet_answers;
    [TextArea(5, 10)] [SerializeField] private string _characterDialogue_completionMet = "";
    [TextArea(1, 2)] [SerializeField] private string[] _characterDialogue_completionMet_answers;

    [Header("Interaction Flags")] 
    public bool firstMet;
    public bool mainMet;
    public bool interceptMet;
    public bool completionMet;

    #endregion

    /**
     * Initialize the dictionary with the character's dialogue for each place.
     */
    public void Init()
    {
        characterAttentionValue = 0;
        firstMet = true;

        characterPlaceSpecificDialogue.Clear();
        characterPlaceSpecificDialogueAnswers.Clear();
        characterGenericDialogueAndAnswers.Clear();

        foreach (PlaceIndexes place in Enum.GetValues(typeof(PlaceIndexes)))
        {
            var dialogueForPlace = GetDialogueForPlace(place);
            var answersForPlace = GetAnswersForPlace(place);

            if (dialogueForPlace is { Length: > 0 })
            {
                characterPlaceSpecificDialogue.Add(place, dialogueForPlace);
            }

            if (answersForPlace is { Length: > 0 })
            {
                characterPlaceSpecificDialogueAnswers.Add(place, answersForPlace);
            }
            Debug.Log("Character Initialized! " + characterName + " " + characterClass + " " + characterAttentionValue);
        }
        
        // TODO: Testing, adjust this later.
        if(_characterDialogue_firstMet != null) characterGenericDialogueAndAnswers.Add(_characterDialogue_firstMet, _characterDialogue_firstMet_answers);
        if(_characterDialogue_interceptMet != null)characterGenericDialogueAndAnswers.Add(_characterDialogue_interceptMet, _characterDialogue_interceptMet_answers);
        if(_characterDialogue_completionMet != null)characterGenericDialogueAndAnswers.Add(_characterDialogue_completionMet, _characterDialogue_completionMet_answers);

        InitializeMoodSprites();
    }

    private void InitializeMoodSprites()
    {
        characterMoodSprite.Clear();
        foreach (CharacterMoods mood in Enum.GetValues(typeof(CharacterMoods)))
        {
            characterMoodSprite.Add(mood, characterMoodSprites[(int)mood]);
        }
    }

    private string[] GetAnswersForPlace(object place)
    {
        var placeIndex = (PlaceIndexes)place;
        return placeIndex switch
        {
            PlaceIndexes.Tavern => _characterDialogue_Tavern_answers,
            PlaceIndexes.MagicShop => _characterDialogue_MagicShop_answers,
            PlaceIndexes.Blacksmith => _characterDialogue_Blacksmith_answers,
            PlaceIndexes.Forest => _characterDialogue_Forest_answers,
            _ => null
        };
    }

    private string[] GetDialogueForPlace(object place)
    {
        var placeIndex = (PlaceIndexes)place;
        return placeIndex switch
        {
            PlaceIndexes.Tavern => _characterDialogue_Tavern,
            PlaceIndexes.MagicShop => _characterDialogue_MagicShop,
            PlaceIndexes.Blacksmith => _characterDialogue_Blacksmith,
            PlaceIndexes.Forest => _characterDialogue_Forest,
            _ => null
        };
    }
}

