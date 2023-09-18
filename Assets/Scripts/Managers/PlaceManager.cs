using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
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
    [SerializeField] private GameObject dragonLairInfoBox;
    
    // TODO: Auslagern in einen UI-Controller
    [SerializeField] private ForestShop _forestShop;

    private PlaceIndexes _currentPlace;
    public UnityEvent onPlaceChanged;

    private void Awake()
    {
        SingletonSetup();
    }

    private void Start()
    {
        ChangePlace(PlaceIndexes.Tavern.ToString());
        Debug.Log("PlaceManager: " + _currentPlace);
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
    
    // TODO: Auslagern in einen UI-Controller
    private void DragonLairInfoTextAppearance()
    {
        var isShowing = dragonLairInfoBox.activeSelf;
        dragonLairInfoBox.SetActive(!isShowing);
    }

    public void ChangePlace(string placeIndex)
    {
        //SetupCanvasGroups();
        //FadeCanvasGroups();

        if (placeIndex == PlaceIndexes.DragonLair.ToString())
        {
            CheckDragonLairRequirement(placeIndex);
        }
        else
        {
            foreach (var place in places)
            {
                if (!IsMatchingPlace(place, placeIndex)) continue;

                _currentPlace = place.placeIndex;
                UpdateUI(place);
                onPlaceChanged.Invoke();
                Debug.Log("PlaceManager 'Change Place': " + _currentPlace);
            }
        }
    }

    private void CheckDragonLairRequirement(string placeIndex)
    {
        if (PlayerManager.Instance.CompanionsCount >= 3)
        {
            foreach (var place in places)
            {
                if (!IsMatchingPlace(place, placeIndex)) continue;

                _currentPlace = place.placeIndex;
                UpdateUI(place);
                onPlaceChanged.Invoke();
                Debug.Log("PlaceManager 'Change Place': " + _currentPlace);
            }
        }
        else
        {
            DragonLairInfoTextAppearance();
            Invoke(nameof(DragonLairInfoTextAppearance), 3);
            Debug.Log("Not enough companions to enter the Dragon Lair!");
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
        if(place == null) return false;
        return place.placeIndex == (PlaceIndexes)Enum.Parse(typeof(PlaceIndexes), placeIndex);
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
        if(targetCanvasGroup.alpha > 0.9f)
            (actualCanvasGroup, targetCanvasGroup) = (targetCanvasGroup, actualCanvasGroup);
    }

    public PlaceIndexes GetCurrentPlace()
    {
        return _currentPlace;
    }
}
