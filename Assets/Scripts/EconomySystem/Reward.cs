using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Reward
{
    public ItemCount[] items;
    private float _percentage = 100f;

    /// <summary>
    /// Spawns items near position
    /// </summary>
    public void GetFullReward(Vector2 pos, int orderInLayer, bool randomize=true)
    {
        foreach(var item in items)
        {
            float x = UnityEngine.Random.Range(-1, 1);
            float y = UnityEngine.Random.Range(-1, 1);
            Vector2 offset = new Vector2(x, y);
            var go = ExtentionMethods.InstantiateWithSortingOrder(item.Item.gameObject, pos, orderInLayer);
            go.GetComponent<Item>().SetItemCount(item.Count);
        }
    }


    /// <summary>
    /// Puts items in inventory
    /// </summary>
    public void GetFullReward(InventoryHandler inventory)
    {
        foreach(var item in items)
        {
            inventory.AddItem(item.Item.Id, item.Count);
        }
    }

    private void GetPartiallyReward(float percentage)
    {
        _percentage-=percentage;
    }
}
