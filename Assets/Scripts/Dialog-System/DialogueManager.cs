using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance { get; private set; }

    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private Button btnLeft;
    [SerializeField] private Button btnRight;
    [SerializeField] private Character _currentCharacterAtPlace;
    [SerializeField] private SingleCanvasController _magicShopPotionCanvas;
    
    // TODO: Auslagern
    [SerializeField] private ForestShop _forestShop;

    private Queue<string> sentencesQueue = new ();
    private Queue<string> answersQueue = new ();
    private string _currentSentence;

    private SO_Character _currentCharacter;
    private PlaceIndexes _currentPlace;

    private void Awake()
    {
        _currentCharacterAtPlace.onCharacterDialogue += InitDialogue;
        
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        btnLeft.onClick.AddListener(DisplayNextSentence);
        btnRight.onClick.AddListener(DisplayNextSentence);
    }
    
    public void RestartDialogue()
    {
        _currentCharacterAtPlace.ActivateCharacterUI();
        InitDialogue(_currentCharacter, _currentPlace);
    }

    public void SkipDialogue()
    {
        EndDialogue();
        RestartDialogue();
    }

    private void InitDialogue(SO_Character character, PlaceIndexes place)
    {
        dialogueBox.SetActive(false);
        
        _currentCharacter = character;
        _currentPlace = place;

        _currentSentence = " ";
        
        dialogueBox.SetActive(true);

        LoadDialogueAndAnswers();
        DisplayNextSentence();
        
        //Debug.Log("InitDialogue: " + _currentCharacter.characterName + " is at " + _currentPlace);
    }

    private void LoadDialogueAndAnswers()
    {
        if (_currentCharacter.firstMet)
        {
            LoadDialogueAndAnswersFromCharacter(
                _currentCharacter._characterDialogue_firstMet,
                _currentCharacter._characterDialogue_firstMet_answers);
            SetCharacterDialogueFlagMainMet();
        }
        else if (_currentCharacter.mainMet)
        {
            LoadDialogueAndAnswersFromCharacter(
                _currentCharacter.characterPlaceSpecificDialogue[_currentPlace], 
                _currentCharacter.characterPlaceSpecificDialogueAnswers[_currentPlace]);
            SetCharacterDialogueFlagInterceptionMet();
        }
        else if (_currentCharacter.interceptMet)
        {
            LoadDialogueAndAnswersFromCharacter(
                _currentCharacter._characterDialogue_interceptMet,
                _currentCharacter._characterDialogue_interceptMet_answers);
        }
        else if (_currentCharacter.completionMet)
        {
            LoadDialogueAndAnswersFromCharacter(
                _currentCharacter._characterDialogue_completionMet,
                _currentCharacter._characterDialogue_completionMet_answers);
        }
        else
        {
           Debug.Log("No dialogue found for this character!");
        }
    }

    public void SetCharacterDialogueFlagFirstMet()
    {
        _currentCharacter.firstMet = true;
        _currentCharacter.mainMet = false;
        _currentCharacter.interceptMet = false;
        _currentCharacter.completionMet = false;
    }
    
    public void SetCharacterDialogueFlagMainMet()
    {
        _currentCharacter.firstMet = false;
        _currentCharacter.mainMet = true;
        _currentCharacter.interceptMet = false;
        _currentCharacter.completionMet = false;
    }
    public void SetCharacterDialogueFlagInterceptionMet()
    {
        _currentCharacter.firstMet = false;
        _currentCharacter.mainMet = false;
        _currentCharacter.interceptMet = true;
        _currentCharacter.completionMet = false;
    }
    
    public void SetCharacterDialogueFlagCompletionMet()
    {
        _currentCharacter.firstMet = false;
        _currentCharacter.mainMet = false;
        _currentCharacter.interceptMet = false;
        _currentCharacter.completionMet = true;
    }

    private void LoadDialogueAndAnswersFromCharacter(string[] dialogue, string[] answers)
    {
        sentencesQueue.Clear();
        answersQueue.Clear();
        
        // Damit SpielerInnen auch nach dem Gespräch Pfeile nehmen können
        if(_currentCharacter.currentCharacterPlace == PlaceIndexes.Forest && _currentCharacter.characterAttentionValue >= 100)
            _forestShop.ShowShop();
        else
        {
            _forestShop.HideShop();
        }

        foreach (var sentence in dialogue)
        {
            sentencesQueue.Enqueue(sentence);
        }

        foreach (var answer in answers)
        {
            answersQueue.Enqueue(answer);
        }
    }

    private void DisplayNextSentence()
    {
        Debug.Log("Sentence Count: " + sentencesQueue.Count);
        if (sentencesQueue.Count > 0)
        {
            _currentSentence = sentencesQueue.Dequeue();
            dialogueText.text = _currentSentence;
            SetAnswerButtonsText();
        }
        else
        {
            if (_currentPlace == PlaceIndexes.MagicShop && _currentCharacter.mainMet)
            {
                if (_magicShopPotionCanvas != null)
                {
                    _magicShopPotionCanvas.SetCanvasRaycasts(true);
                    _magicShopPotionCanvas.showingCanvas = true;
                }
                else
                {
                    Debug.Log("Magic Shop Potion Canvas is null!");
                }
            }

            if (_currentPlace == PlaceIndexes.DragonLair && _currentCharacter.completionMet)
            {
                SceneManager.LoadScene(SceneIndexes.CREDITS.ToString());
            }
            EndDialogue();
        }
    }

    private void EndDialogue()
    {
        dialogueBox.SetActive(false);
        _currentCharacterAtPlace.DeactivateCharacterUI();
        switch (_currentCharacter.currentCharacterPlace)
        {
           case PlaceIndexes.Tavern:
               if (_currentCharacter.characterAttentionValue >= 100 && !_currentCharacter.completionMet)
               {
                   SetCharacterDialogueFlagCompletionMet();
                   PlayerManager.Instance.AddCompanion();
                   RestartDialogue();
               }
               break;
           case PlaceIndexes.MagicShop:
               if (_currentCharacter.mainMet)
               {
                   _currentCharacter.characterAttentionValue = 100;
                   SetCharacterDialogueFlagInterceptionMet();
                   RestartDialogue();
                   PlayerManager.Instance.AddCompanion();
                   SetCharacterDialogueFlagCompletionMet();
               }
               break;
           case PlaceIndexes.Forest:
               if (_currentCharacter.mainMet)
               {
                   SetCharacterDialogueFlagInterceptionMet();
                   RestartDialogue();
               }
               if (_currentCharacter.interceptMet)
               { 
                   _forestShop.ShowShop();
                   _currentCharacter.characterAttentionValue = 100;
                   SetCharacterDialogueFlagCompletionMet();
                   PlayerManager.Instance.AddCompanion();
               }
               if(_currentCharacter.completionMet && _currentCharacter.characterAttentionValue >= 100)
               {
                   _forestShop.ShowShop();
               }
               break;
           case PlaceIndexes.DragonLair:
               if (_currentCharacter.mainMet)
               {
                   Debug.Log("Dragon Lair: Main Met");
                   RestartDialogue();
               }
               if (_currentCharacter.interceptMet)
               {
                   Debug.Log("Dragon Lair: Intercept Met");
                   RestartDialogue();
                   SetCharacterDialogueFlagCompletionMet();
               }
               break;
           default:
               Debug.Log("Dialogue Manager: Character Class not found!");
               break;
        }
    }

    private void SetAnswerButtonsText()
    {
        if (answersQueue.Count < 2) return;
        btnLeft.GetComponentInChildren<TextMeshProUGUI>().text = answersQueue.Dequeue();
        btnRight.GetComponentInChildren<TextMeshProUGUI>().text = answersQueue.Dequeue();
    }
}