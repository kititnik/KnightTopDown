using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryEvents
{
    public event Action<int, InventoryEvent> onInventoryChange;
    public void InventoryChange(int itemId, InventoryEvent inventoryEvent) 
    {
        onInventoryChange?.Invoke(itemId, inventoryEvent);
    }
}
