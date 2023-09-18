using UnityEngine;

[CreateAssetMenu(fileName = "New Story Place", menuName = "Place")]
public class SO_Places : ScriptableObject
{
    [SerializeField] public Sprite backgroundSprite;
    [SerializeField] public Sprite characterSprite;
    [SerializeField] public PlaceIndexes placeIndex;
    [Header("Optional")]
    [SerializeField] public string placeDescription;
}
