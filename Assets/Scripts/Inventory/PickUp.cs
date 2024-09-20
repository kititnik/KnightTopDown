using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(InventoryHandler))]
public class PickUp : MonoBehaviour
{
    private InventoryHandler _inventory;

    private void Awake()
    {
        _inventory = GetComponent<InventoryHandler>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.GetComponent<Item>() == null) return;
        var itemComponent = col.gameObject.GetComponent<Item>();
        _inventory.AddItem(itemComponent.Id);
        itemComponent.OnPickUp();
    }
}