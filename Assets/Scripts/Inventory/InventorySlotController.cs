using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlotController : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        print("InventorySlotController: OnDrop called");
        // If the slot is empty, set the parent of the item to the slot
        if (transform.childCount != 0) return;
        var inventoryItem = eventData.pointerDrag.GetComponent<InventoryItem>();
        inventoryItem.parentAfterDrag = transform;
        CheckComparison(inventoryItem.GetItem());
    }

    private void CheckComparison(SO_Item item)
    {
        if(item.itemName == "Arrows") print("Item dropped: " + item.itemName);
    }
}