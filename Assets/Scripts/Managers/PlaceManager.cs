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
    [SerializeField] private Image sceneBackground;
    [SerializeField] private TextMeshProUGUI placeName;
    [SerializeField] private CanvasGroup actualCanvasGroup, targetCanvasGroup;
    [SerializeField] private CanvasGroupAlphaFade canvasGroupAlphaFade;

    private PlaceIndexes _currentPlace;
    public UnityEvent onPlaceChanged;

    private void Awake()
    {
        SingletonSetup();
    }

    private void SingletonSetup()
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
        SetupCanvasGroups();
        FadeCanvasGroups();

        foreach (var place in places)
        {
            if (!IsMatchingPlace(place, placeIndex)) continue;

            SetCurrentPlace(place.placeIndex);
            UpdateUI(place);
            InvokePlaceChangedEvent();
        }
    }

    private void SetupCanvasGroups()
    {
        canvasGroupAlphaFade.canvasGroupFadeOut = actualCanvasGroup;
        canvasGroupAlphaFade.canvasGroupFadeIn = targetCanvasGroup;
        canvasGroupAlphaFade.lerpTime = 1f;
    }

    private void FadeCanvasGroups()
    {
        canvasGroupAlphaFade.FadeCanvasGroup();
        SwapCanvasGroups();
    }

    private bool IsMatchingPlace(SO_Places place, string placeIndex)
    {
        return place.placeIndex == (PlaceIndexes)Enum.Parse(typeof(PlaceIndexes), placeIndex);
    }

    private void SetCurrentPlace(PlaceIndexes placeIndex)
    {
        _currentPlace = placeIndex;
    }

    private void UpdateUI(SO_Places place)
    {
        sceneBackground.sprite = place.backgroundSprite;
        placeName.text = place.placeIndex.ToString();
    }

    private void InvokePlaceChangedEvent()
    {
        onPlaceChanged.Invoke();
    }

    private void SwapCanvasGroups()
    {
        (actualCanvasGroup, targetCanvasGroup) = (targetCanvasGroup, actualCanvasGroup);
    }

    public PlaceIndexes GetCurrentPlace()
    {
        return _currentPlace;
    }
}
