using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemInteraction : MonoBehaviour, IDropHandler
{
    
    [SerializeField] private Character _character;
    [SerializeField] private int _attentionValue = 10;
    private InventoryItem _inventoryItem;
    
    public void OnDrop(PointerEventData eventData)
    {
        _inventoryItem = eventData.pointerDrag.GetComponent<InventoryItem>();
        if (_inventoryItem == null) return;
        print("Interacting with character");
        CheckCharacterUsage();
        
        Destroy(_inventoryItem.gameObject);
    }

    private void CheckCharacterUsage()
    {
        // TODO: Check if the item can be used by the character
        print(_inventoryItem.GetItem().itemName);
        foreach (var item in _character.GetDesiredObjects())
        {
            if (item == _inventoryItem.GetItem())
            {
                print("Item is desired by character");
                _character.ChangeAttentionValue(_attentionValue);
                return;
            }
        }
    }
}
