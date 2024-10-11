using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Interact : MonoBehaviour
{
    [SerializeField] private GameObject interactiveBtn;
    [SerializeField] private InteractionUI interactionUI;
    private IInteractable _interactable;
    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        var newCollision = collider2D.GetComponent<IInteractable>();
        if(newCollision == null) return;
        _interactable = newCollision;
        interactiveBtn.SetActive(true);
        _interactable.OnNearObject(gameObject, interactionUI);
    }
    private void OnTriggerExit2D(Collider2D collider2D)
    {
        var interactable = collider2D.GetComponent<IInteractable>();
        if(interactable == null) return;
        interactable.OnExitedObject(gameObject, interactionUI);
        if(interactiveBtn!=null) interactiveBtn.SetActive(false);
        _interactable = null;
    }

    public void Send()
    {
        if(_interactable == null) return;
        interactiveBtn.SetActive(false);
        _interactable.Invoke(gameObject, interactionUI);
    }
}
