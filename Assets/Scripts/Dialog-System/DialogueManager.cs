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

    private Queue<string> _sentences;
    private Queue<string> _answers;
    private string _currentSentence;
    private string _currentAnswer;
    private SO_Character _currentCharacter;
    private PlaceIndexes _currentPlace;
    private string[] _currentDialogue;
    private string[] _currentDialogueAnswers;
    private int _currentDialogueIndex;
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

        _sentences = new Queue<string>();
        _answers = new Queue<string>();

        // Setup the button listeners
        btnLeft.onClick.AddListener(DisplayNextSentence);
        btnRight.onClick.AddListener(DisplayNextSentence);
    }

    private void InitDialogue(SO_Character character, PlaceIndexes place)
    {
        _currentCharacter = character;
        _currentPlace = place;

        // Array of sentences Strings from the SO_Character Object
        _currentDialogue = _currentCharacter.characterPlaceSpecificDialogue[_currentPlace];

        // Array of answers Strings from the SO_Character Object
        _currentDialogueAnswers = _currentCharacter.characterPlaceSpecificDialogueAnswers[_currentPlace];

        _currentDialogueIndex = 0;
        dialogueBox.SetActive(true);

        // Clear the sentences queue
        _sentences.Clear();

        // Add the sentences to the queue
        foreach (var sentence in _currentDialogue)
        {
            _sentences.Enqueue(sentence);
        }

        // Add the answers to the buttons
        foreach (var answer in _currentDialogueAnswers)
        {
            _answers.Enqueue(answer);
        }

        // Display the first sentence.
        // This will also increment the current dialogue index
        // Use Spacebar to continue the dialogue
        DisplayNextSentence();
    }

    // Display the next sentence
    private void DisplayNextSentence()
    {
        print("DisplayNextSentence() called ::: _sentences.Count = " + _sentences.Count +
              " ::: _currentDialogueIndex = " + _currentDialogueIndex + " ::: _currentDialogue.Length = " +
              _currentDialogue.Length);
        // If there are no more sentences
        if (_sentences.Count == 0)
        {
            // End the dialogue
            EndDialogue();
            return;
        }

        // Get the next sentence
        _currentSentence = _sentences.Dequeue();
        // Set the dialogue text
        dialogueText.text = _currentSentence;

        // Set button text
        SetAnswerButtonsText(_currentDialogueIndex);

        if (_currentDialogueIndex < _currentDialogueAnswers.Length)
        {
            // Increment the current dialogue index, so the next PAIR of answers will be displayed
            _currentDialogueIndex += 2;
        }
    }

    // End the dialogue
    private void EndDialogue()
    {
        // Set the dialogue box inactive
        dialogueBox.SetActive(false);
        // Deactivate the current character's Image
        _currentCharacterAtPlace.DeactivateCharacter();
        // If the current dialogue index is less than the current dialogue length
        if (_currentDialogueIndex >= _currentDialogue.Length) return;

        // Increment the current dialogue index
        _currentDialogueIndex = 0;
    }

    // Current Workaround for the DialogueManager
    private void SetAnswerButtonsText(int currentIndex)
    {
        // Set button text
        btnLeft.GetComponentInChildren<TextMeshProUGUI>().text = _currentDialogueAnswers[currentIndex];
        btnRight.GetComponentInChildren<TextMeshProUGUI>().text = _currentDialogueAnswers[currentIndex + 1];
    }
}