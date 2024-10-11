using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

[Serializable]
public class Price
{
    public ItemCount[] items;

    public bool CheckPayingAbility(InventoryHandler inventory)
    {
        for(int i = 0; i < items.Length; i++)
        {
            int invItemCount = inventory.GetItemCount(items[i].Item.Id);
            if(invItemCount < items[i].Count) return false;
        }
        return true;
    }

    public bool TryTakePrice(InventoryHandler inventory)
    {
        if(!CheckPayingAbility(inventory)) return false;
        for(int i = 0; i < items.Length; i++)
        {
            inventory.RemoveItem(items[i].Item.Id, items[i].Count);
        }
        return true;
    }

    public override string ToString()
    {
        var sb = new StringBuilder();
        for(int i = 0; i < items.Length; i++)
        {
            sb.AppendLine($"{items[i].Item.DisplayName} x{items[i].Count}");
        }
        return sb.ToString();
    }
}
