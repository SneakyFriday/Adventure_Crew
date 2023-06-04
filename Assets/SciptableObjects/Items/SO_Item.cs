using UnityEngine;

[CreateAssetMenu(menuName = "Item")]
public class SO_Item : ScriptableObject
{
    [Header("Item Properties")]
    public string itemName;
    public Sprite itemSprite;
    [Header("Character Properties")]
    public CharacterItemType characterItemType;
    public bool isCharacterItem;
    [Header("Inventory-UI")]
    public bool stackable;
}

public enum CharacterItemType
{
    Tank,
    Healer,
    DamageDealer,
}


