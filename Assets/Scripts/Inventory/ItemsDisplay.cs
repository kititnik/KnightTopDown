using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class ItemsDisplay : MonoBehaviour
{
    [SerializeField] private ItemsDisplayElement[] itemsDisplayElements;
    private void Start()
    {
        EventsManager.instance.InventoryEvents.OnInventoryChange += OnInventoryUpdate;
    }
    
    private void OnInventoryUpdate(int itemId, int count, InventoryEvent kind)
    {
        foreach (var e in itemsDisplayElements)
        {
            if (e.itemId != itemId) continue;
            int currentCount = Convert.ToInt32(e.countText.text);
            int finalCount = currentCount;
            if(kind == InventoryEvent.AddItem) finalCount += count;
            else if (kind == InventoryEvent.RemoveItem) finalCount -= count;
            e.countText.text = (finalCount).ToString();
            return;
        }
    }
}

[Serializable]
internal class ItemsDisplayElement
{
    public int itemId;
    public TMP_Text countText;

    public ItemsDisplayElement()
    {
        itemId = -1;
    }
    
}