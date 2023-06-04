using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnTest : MonoBehaviour
{
    public InventoryManager inventoryManager;
    public SO_Item[] items;
    
    public void AddItemToInventory(int id)
    {
        var result = inventoryManager.AddItemToInventory(items[id]);
        print(result ? "Item added to inventory" : "Item not added to inventory");
    }
}
