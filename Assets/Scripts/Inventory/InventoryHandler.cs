using System.Collections.Generic;
using UnityEngine;

public class InventoryHandler : MonoBehaviour
{
    private Inventory _inventory;
    [SerializeField] private int inventorySize;

    private void Awake()
    {
        _inventory = new Inventory(inventorySize);
    }
    
    public bool AddItem(int itemId, int count)
    {
        bool success = _inventory.AddItem(itemId, count);
        if(success) NotifyObservers(itemId, count, InventoryEvent.AddItem);
        return success;
    }

    public bool RemoveItem(int itemId)
    {
        bool success = _inventory.RemoveItem(itemId);
        if(success) NotifyObservers(itemId, 1, InventoryEvent.RemoveItem);
        return success;
    }

    public bool RemoveItem(int itemId, int count)
    {
        for(int i = 0; i < count; i++)
        {
            bool success = _inventory.RemoveItem(itemId);
            if(!success) return false;  
        }
        NotifyObservers(itemId, count, InventoryEvent.RemoveItem);
        return true;
    }

    public int GetItemCount(int itemId)
    {
        return _inventory.GetItemCount(itemId);
    }

    private void NotifyObservers(int itemId, int count, InventoryEvent kind)
    {
        EventsManager.instance.InventoryEvents.InventoryChange(itemId, count, kind);
    }
}