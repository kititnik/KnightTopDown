using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryEvents
{
    public event Action<int, int, InventoryEvent> OnInventoryChange;
    public void InventoryChange(int itemId, int count, InventoryEvent inventoryEvent) 
    {
        OnInventoryChange?.Invoke(itemId, count, inventoryEvent);
    }
}
