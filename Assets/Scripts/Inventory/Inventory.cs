using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Inventory : IEnumerable
{
    [SerializeField]
    private List<InventoryItem> _inventory;
    public IEnumerator GetEnumerator() => _inventory.GetEnumerator();

    public Inventory(int size)
    {
        _inventory = new List<InventoryItem>(size);
    }

    public IEnumerable<InventoryItem> GetSlots()
    {
        return _inventory;
    }

    private int FindItemIndex(int itemId)
    {
        for (int i = 0; i < _inventory.Count; i++)
        {
            if (_inventory[i].ItemId == itemId) return i;
        }
        return -1;
    }

    public bool IsInventoryHasItem(int itemId)
    {
        return FindItemIndex(itemId) == -1 ? false : true;
    }

    public bool RemoveItem(int itemId)
    {
        int i = FindItemIndex(itemId);
        if (i == -1) return false;
        _inventory[i].Count--;
        if(_inventory[i].Count == 0) _inventory.RemoveAt(i);
        return true;
    }

    public bool AddItem(int itemId)
    {
        int i = FindItemIndex(itemId);
        if(i == -1) _inventory.Add(new InventoryItem(itemId, 1));
        else _inventory[i].Count++;
        return true;
    }

    public int GetItemCount(int itemId)
    {
        int i = FindItemIndex(itemId);
        if(i==-1) return 0;
        return _inventory[i].Count;
    }
}
