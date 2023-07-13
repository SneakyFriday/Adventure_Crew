using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    // DialogueManager is a singleton
    public static DialogueManager Instance;

    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private Button btnLeft;
    [SerializeField] private Button btnRight;

    // // Setup the Character Dialogue Queues
    // private Queue<string> _sentencesQueueFirstMet;
    // private Queue<string> _sentencesQueueMainMet;
    // private Queue<string> _sentencesQueueInterceptMet;
    // private Queue<string> _sentencesQueueCompletionMet;
    //
    // // Setup the Player/Button Answers Queues
    // private Queue<string> _answersQueueFirstMet;
    // private Queue<string> _answersQueueMainMet;
    // private Queue<string> _answersQueueInterceptMet;
    // private Queue<string> _answersQueueCompletionMet;

    private SentenceAnswer _sentenceFirstMet;
    private SentenceAnswer _sentenceMainMet;
    private SentenceAnswer _sentenceInterceptMet;
    private SentenceAnswer _sentenceCompletionMet;
    
    // Sentences and Answers Properties
    private string _currentSentence;
    private string _currentAnswer;
    private string[] _currentFirstDialogue;
    private string[] _currentFirstDialogueAnswers;
    private string[] _currentMainDialogue;
    private string[] _currentMainDialogueAnswers;
    private string[] _currentInterceptDialogue;
    private string[] _currentInterceptDialogueAnswers;
    private string[] _currentCompletionDialogue;
    private string[] _currentCompletionDialogueAnswers;
    private int _currentDialogueIndex;
    
    // Character and Place Properties
    private SO_Character _currentCharacter;
    private PlaceIndexes _currentPlace;
    private Character _currentCharacterAtPlace;

    private void Awake()
    {
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
        // Setup the character and dialogue event listeners
        _currentCharacterAtPlace = FindObjectOfType<Character>();
        _currentCharacterAtPlace.onCharacterDialogue += InitDialogue;

        // // Setup the dialogue queues
        // _sentencesQueueFirstMet = new Queue<string>();
        // _sentencesQueueMainMet = new Queue<string>();
        // _sentencesQueueInterceptMet = new Queue<string>();
        // _sentencesQueueCompletionMet = new Queue<string>();
        //
        // // Setup the answers queues
        // _answersQueueFirstMet = new Queue<string>();
        // _answersQueueMainMet = new Queue<string>();
        // _answersQueueInterceptMet = new Queue<string>();
        // _answersQueueCompletionMet = new Queue<string>();

        // Setup the button listeners
        btnLeft.onClick.AddListener(DisplayNextSentence);
        btnRight.onClick.AddListener(DisplayNextSentence);
    }

    private void InitDialogue(SO_Character character, PlaceIndexes place)
    {
        _currentCharacter = character;
        _currentPlace = place;

        // Array of sentences Strings from the SO_Character Object
        _currentMainDialogue = _currentCharacter.characterPlaceSpecificDialogue[_currentPlace];


        // Array of answers Strings from the SO_Character Object
        _currentMainDialogueAnswers = _currentCharacter.characterPlaceSpecificDialogueAnswers[_currentPlace];
        _currentDialogueIndex = 0;
        dialogueBox.SetActive(true);

        // // Clear the sentences queue
        // _sentencesQueueMainMet.Clear();
        //
        // // Add the sentences to the queue
        // foreach (var sentence in _currentMainDialogue)
        // {
        //     _sentencesQueueMainMet.Enqueue(sentence);
        // }
        //
        // // Add the answers to the buttons
        // foreach (var answer in _currentMainDialogueAnswers)
        // {
        //     _answersQueueMainMet.Enqueue(answer);
        // }
        
        DisplayNextSentence();
    }

    // Display the next sentence
    private void DisplayNextSentence()
    {
        // // Check Character Flags
        // if(_currentCharacter.firstMet) Dialogue(_sentencesQueueFirstMet, _answersQueueFirstMet);
        // else if(_currentCharacter.mainMet) Dialogue(_sentencesQueueMainMet, _answersQueueMainMet);
        // else if(_currentCharacter.interceptMet) Dialogue(_sentencesQueueInterceptMet, _answersQueueInterceptMet);
        // else if(_currentCharacter.completionMet) Dialogue(_sentencesQueueCompletionMet, _answersQueueCompletionMet);
    }

    private void Dialogue(Queue<string> sentencesQueue, Queue<string> answersQueue)
    {
        if (_currentDialogueIndex < _currentMainDialogue.Length)
        {
            _currentSentence = sentencesQueue.Dequeue();
            _currentAnswer = answersQueue.Dequeue();
            dialogueText.text = _currentSentence;
            
            SetAnswerButtonsText(_currentDialogueIndex + 1, answersQueue.ToArray());
            
            _currentDialogueIndex++;
        }
        else EndDialogue();
    }
    
    private void EndDialogue()
    {
        dialogueBox.SetActive(false);
        // Deactivate the current character's Image
        _currentCharacterAtPlace.DeactivateCharacter();
        if (_currentDialogueIndex >= _currentMainDialogue.Length) return;
        _currentDialogueIndex = 0;
    }

    // Current Workaround for the DialogueManager
    private void SetAnswerButtonsText(int currentIndex, string[] answers)
    {
        btnLeft.GetComponentInChildren<TextMeshProUGUI>().text = answers[currentIndex];
        btnRight.GetComponentInChildren<TextMeshProUGUI>().text = answers[currentIndex + 1];
    }
}