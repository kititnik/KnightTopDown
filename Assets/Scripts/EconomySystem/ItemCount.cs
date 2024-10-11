using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ItemCount
{
    public Item Item;
    public int Count;

    public ItemCount(Item item, int count)
    {
        Item = item;
        Count = count;
    }
}
