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
