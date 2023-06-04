using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    #region Variables

    [SerializeField] private GameObject _loadScreenCanvas;
    [SerializeField] private Image _loadingBar;
    [SerializeField] private Scene  _currentScene;
    private float _progress;
    
    public static LevelManager Instance;

    #endregion
    
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
    
    /**
     * <summary>
     * Ein Ladescreen für so ein Spiel? Das ist doch nicht nötig!
     * Aber ich hatte Lust das Prinzip zu lernen und zu implementieren ^^
     * </summary>
     */

    public void LoadGame()
    {
        SceneManager.UnloadSceneAsync((int) SceneIndexes.STARTSCREEN);
        SceneManager.LoadSceneAsync((int)SceneIndexes.GAMESCREEN, LoadSceneMode.Additive);
        
    }
    
    public async void LoadScene(string sceneName)
    {
        // Setzt den Ladebalken zurück
        _progress = 0f;
        _loadingBar.fillAmount = 0f;
        
        // Lädt die Szene asynchron
        var scene = SceneManager.LoadSceneAsync(sceneName);
        scene.allowSceneActivation = false;
        _loadScreenCanvas.SetActive(true);

        do
        {
            await Task.Delay(1000);
            _progress = scene.progress;
        }
        while (scene.progress < 0.9f);

        scene.allowSceneActivation = true;
        _loadScreenCanvas.SetActive(false);
    }

    private void Update()
    {
        // Ladebalken kontinuierlich füllen. Sieht cooler aus.
        _loadingBar.fillAmount = Mathf.MoveTowards(_loadingBar.fillAmount, _progress, Time.deltaTime * 3);
    }
}
