using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Story Character", menuName = "Character")]
[Serializable]
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
        Warrior,
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
    [TextArea(5, 10)] [SerializeField] private string[] _characterDialogue_Forest;
    [TextArea(1, 2)] [SerializeField] private string[] _characterDialogue_Forest_answers;
    [TextArea(5, 10)] [SerializeField] private string[] _characterDialogue_MagicShop;
    [TextArea(1, 2)] [SerializeField] private string[] _characterDialogue_MagicShop_answers;
    [TextArea(5, 10)] [SerializeField] private string[] _characterDialogue_Blacksmith;
    [TextArea(1, 2)] [SerializeField] private string[] _characterDialogue_Blacksmith_answers;
    [TextArea(5, 10)] [SerializeField] private string[] _characterDialogue_DragonHort;
    [TextArea(1, 2)] [SerializeField] private string[] _characterDialogue_DragonHort_answers;
    
    [Header("Generic Dialogue for Testing Purposes")]
    [TextArea(5, 10)] [SerializeField] public string[] _characterDialogue_firstMet;
    [TextArea(1, 2)] [SerializeField] public string[] _characterDialogue_firstMet_answers;
    [TextArea(5, 10)] [SerializeField] public string[] _characterDialogue_interceptMet;
    [TextArea(1, 2)] [SerializeField] public string[] _characterDialogue_interceptMet_answers;
    [TextArea(5, 10)] [SerializeField] public string[] _characterDialogue_completionMet;
    [TextArea(1, 2)] [SerializeField] public string[] _characterDialogue_completionMet_answers;

    [Header("Interaction Flags")] 
    [SerializeField] public bool firstMet;
    [SerializeField] public bool mainMet;
    [SerializeField] public bool interceptMet;
    [SerializeField] public bool completionMet;

    #endregion

    /**
     * Initialize the dictionary with the character's dialogue for each place.
     */
    public void Init()
    {
        characterAttentionValue = 0;
        firstMet = true;

        ResetCharacterDialogueFlags();
        characterPlaceSpecificDialogue.Clear();
        characterPlaceSpecificDialogueAnswers.Clear();
        characterGenericDialogueAndAnswers.Clear();

        foreach (PlaceIndexes place in Enum.GetValues(typeof(PlaceIndexes)))
        {
            
            //ResetCharacterDialogueFlags();
            
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
        }
        
        // TODO: Testing, adjust this later.
        // Put the generic dialogue and answers into the dictionary
        
        //if(_characterDialogue_firstMet != null) characterGenericDialogueAndAnswers.Add(_characterDialogue_firstMet[0], _characterDialogue_firstMet_answers);
        //if(_characterDialogue_interceptMet != null)characterGenericDialogueAndAnswers.Add(_characterDialogue_interceptMet[0], _characterDialogue_interceptMet_answers);
        //if(_characterDialogue_completionMet != null)characterGenericDialogueAndAnswers.Add(_characterDialogue_completionMet[0], _characterDialogue_completionMet_answers);
        
        //if(_characterDialogue_firstMet != null) characterGenericDialogueAndAnswers.Add(_characterDialogue_firstMet, _characterDialogue_firstMet_answers);
        //if(_characterDialogue_interceptMet != null)characterGenericDialogueAndAnswers.Add(_characterDialogue_interceptMet, _characterDialogue_interceptMet_answers);
        //if(_characterDialogue_completionMet != null)characterGenericDialogueAndAnswers.Add(_characterDialogue_completionMet, _characterDialogue_completionMet_answers);

        //InitializeMoodSprites();
    }

    public void ResetCharacterDialogueFlags()
    {
        firstMet = true;
        mainMet = false;
        interceptMet = false;
        completionMet = false;
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
            PlaceIndexes.DragonLair => _characterDialogue_DragonHort_answers,
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
            PlaceIndexes.DragonLair => _characterDialogue_DragonHort,
            _ => null
        };
    }
}

