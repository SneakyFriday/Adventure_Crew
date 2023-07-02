using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    //[SerializeField] private List<SO_Character> characters = new();
    
    #region Singleton

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

    #endregion
    
    private void Start()
    {
        // Play Intro Music
        SoundManager.Instance.PlayBGM(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void StartGame()
    {
        LevelManager.Instance.LoadScene(SceneIndexes.GAMESCREEN.ToString());
    }
    
    public void LoadIntro()
    {
        LevelManager.Instance.LoadScene(SceneIndexes.INTROSCREEN.ToString());
    }

    public void LoadMainMenu()
    {
        LevelManager.Instance.LoadScene(SceneIndexes.STARTSCREEN.ToString());
    }

    public void LoadCredits()
    {
        LevelManager.Instance.LoadScene(SceneIndexes.CREDITS.ToString());
    }
}