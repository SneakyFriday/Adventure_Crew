using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private GameObject inventoryContainer;
    [SerializeField] private GameObject itemPrefab;
    private InventorySlotController[] _inventoryItemSlots;

    private void Start()
    {
       var childCount = inventoryContainer.transform.childCount;
       _inventoryItemSlots = new InventorySlotController[childCount];
       for(var i = 0; i < childCount; i++)
       {
           _inventoryItemSlots[i] = inventoryContainer.transform.GetChild(i).GetComponent<InventorySlotController>();
       }
    }

    /**
     * Add an item to the inventory
     * @param item The item to add
     * @return void
     */
    public bool AddItemToInventory(SO_Item item)
    {
        foreach (var slot in _inventoryItemSlots)
        {
            if (slot.transform.childCount != 0) continue;
            SpawnNewItem(item, slot);
            return true;
        }
        return false;
    }

    /**
     * Spawn a new item in the inventory
     * @param item The item to spawn
     * @param slot The slot to spawn the item in
     * @return void
     */
    private void SpawnNewItem(SO_Item item, InventorySlotController slot)
    {
        var newItem = Instantiate(itemPrefab, slot.transform);
        var inventoryItem = newItem.GetComponent<InventoryItem>();
        inventoryItem.InitItem(item);
    }
}
