using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(InventoryHandler))]
public class PickUp : MonoBehaviour
{
    private InventoryHandler _inventory;
    private InteractionUI _interactionUI;

    private void Awake()
    {
        _inventory = GetComponent<InventoryHandler>();
        _interactionUI = FindObjectOfType<InteractionUI>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.GetComponent<Item>() == null) return;
        var itemComponent = col.gameObject.GetComponent<Item>();
        _inventory.AddItem(itemComponent.Id, itemComponent.Count);
        _interactionUI.AddNoification($"{itemComponent.DisplayName} x{itemComponent.Count}", 4f);
        itemComponent.OnPickUp();
    }
}