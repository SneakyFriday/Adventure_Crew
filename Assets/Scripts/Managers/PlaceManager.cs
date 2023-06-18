using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlaceManager : MonoBehaviour
{
    
    // PlaceManager is a singleton
    public static PlaceManager Instance;
    
    [Tooltip("Add your Places for the game here.")]
    [SerializeField] private SO_Places[] places;
    [Tooltip("Place Scene Background Object here")]
    [SerializeField] private Image sceneBackground;
    [Tooltip("Place Name Object here")]
    [SerializeField] private TextMeshProUGUI placeName;
    [Tooltip("CanvasGroup for Fade In Fade Out")]
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private CanvasGroupAlphaFade canvasGroupAlphaFade;

    private PlaceIndexes _currentPlace;
    
    public UnityEvent onPlaceChanged;

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
    
    public void ChangePlace(string placeIndex)
    {
        
        // Fade In fade Out
        canvasGroupAlphaFade.canvasGroup = canvasGroup;
        canvasGroupAlphaFade.startAlpha = 0f;
        canvasGroupAlphaFade.endAlpha = 1f;
        canvasGroupAlphaFade.lerpTime = 1f;
        canvasGroupAlphaFade.FadeCanvasGroup();
        
        foreach (var place in places)
        {
            if (place.placeIndex != (PlaceIndexes)Enum.Parse(typeof(PlaceIndexes), placeIndex)) continue;
            
            _currentPlace = place.placeIndex;
            sceneBackground.sprite = place.backgroundSprite;
            placeName.text = place.placeIndex.ToString();
            onPlaceChanged.Invoke();
        }
    }
    
    public PlaceIndexes GetCurrentPlace()
    {
        return _currentPlace;
    }
}
