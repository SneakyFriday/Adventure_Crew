using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Story Place", menuName = "Place")]
public class SO_Places : ScriptableObject
{
    public Sprite backgroundSprite;
    public Sprite characterSprite;
    public PlaceIndexes placeIndex;
    [Header("Optional")]
    public string placeDescription;
}
