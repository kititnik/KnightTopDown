using System;
using UnityEngine;

[Serializable]
public class InventoryItem
{
    public int ItemId;
    public int Count;

    public InventoryItem(int itemId, int count)
    {
        ItemId = itemId;
        Count = count;
    }
}