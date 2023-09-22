using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

/**
 * This class is responsible for saving and loading the game.
 * It uses the Character Object to get the character data.
 * Needs to be JSON because BinaryFormatter is unsafe.
 * Needs to be a List instead of an Dictionary because Dictionary is not serializable.
 * Will be fully implemented in the next version.
 */

[Serializable]
internal class CharacterDataContainer
{
    [SerializeField] public string characterName;
    [SerializeField] public int characterAttentionValue;
}

public class SaveGameManager : MonoBehaviour
{
    [SerializeField] private Character _characterObject;
    [SerializeField] private Button saveButton, loadButton;
    [SerializeField] private List<SO_Character> _characters;
    [SerializeField] private List<CharacterDataContainer> _characterDataContainer;
    [SerializeField] private string _filePatch;

    private void Awake()
    {
        _filePatch = $"{Application.persistentDataPath}/savegame.json";
        _characterDataContainer = new List<CharacterDataContainer>();
    }

    private void Start()
    {
        saveButton.onClick.AddListener(Save);
        loadButton.onClick.AddListener(Load);

        // Get the character data from the Character Object
        _characters = new List<SO_Character>();
        foreach (var character in _characterObject.GetCharacterData())
        {
            _characters.Add(character);
            //print("SaveGameManager:::Character added to list: " + character.characterName);
            //print("SaveGameManager:::Character attention value: " + character.characterAttentionValue);
            //print("Characters: " + _characters);
        }
    }

    private void Load()
    {
        if(File.Exists(_filePatch))
        {
            // Read the JSON file
            var json = File.ReadAllText(_filePatch);
            // Convert the JSON string to a SaveGame object
            _characterDataContainer = JsonUtility.FromJson<List<CharacterDataContainer>>(json);
            // Loop through the SaveGame object and set the data to the Character Object
            // TODO: Fix this foreach loop
            foreach (var character in _characterDataContainer)
            {
                foreach (var characterObject in _characters)
                {
                    if (characterObject.characterName != character.characterName) continue;
                    characterObject.characterAttentionValue = character.characterAttentionValue;
                    //print("SaveGameManager:::Character attention value: " + characterObject.characterAttentionValue);
                }
            }
        }
        else
        {
            Debug.LogError("SaveGameManager:::Save file not found in " + _filePatch);
        }
    }

    private void Save()
    {
        foreach (var character in _characters)
        {
            var data = new CharacterDataContainer
            {
                characterName = character.characterName,
                characterAttentionValue = character.characterAttentionValue,
            };
            
            // Add the data to the SaveGame object
            _characterDataContainer.Add(data);
        }
        
        // Convert the SaveGame object to a JSON string
        var json = JsonUtility.ToJson(_characterDataContainer);
        File.WriteAllText(_filePatch, json);
    }
}