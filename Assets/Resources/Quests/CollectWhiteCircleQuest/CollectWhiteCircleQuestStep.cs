using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectApplesQuestStep : QuestStep
{
    private int itemsCollected = 0;
    private int itemsToComplete = 1;
    private int itemsId = 0;
    private InventoryHandler _inventoryHandler;

    public override void Initialize()
    {
        EventsManager.instance.InventoryEvents.onInventoryChange += OnInventoryUpdate;
        _inventoryHandler = FindAnyObjectByType<InventoryHandler>();
        itemsCollected = _inventoryHandler.GetItemCount(itemsId);
        if(itemsCollected >= itemsToComplete)
        {
            FinishQuestStep();
            _inventoryHandler.RemoveItem(itemsId);
        }
    }

    private void OnDisable()
    {
        EventsManager.instance.InventoryEvents.onInventoryChange -= OnInventoryUpdate;
    }

    public void OnInventoryUpdate(int itemId, InventoryEvent kind)
    {
        if(itemId == itemsId && kind == InventoryEvent.AddItem)
        {
            itemsCollected++;
        }
        if(itemId == itemsId && kind == InventoryEvent.RemoveItem)
        {
            itemsCollected--;
        }
        if(itemsCollected >= itemsToComplete)
        {
            FinishQuestStep();
            _inventoryHandler.RemoveItem(itemsId);
        }
    }
}