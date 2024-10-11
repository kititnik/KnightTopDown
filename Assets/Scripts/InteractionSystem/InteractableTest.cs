using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableTest : MonoBehaviour, IInteractable
{
    public void Invoke(GameObject player, InteractionUI interactionUI)
    {
        Debug.Log("Interaction with " + gameObject.name);
    }

    public void OnExitedObject(GameObject player, InteractionUI interactionUI)
    {
        Debug.Log(gameObject.name + " exited");
    }

    public void OnNearObject(GameObject player, InteractionUI interactionUI)
    {
        Debug.Log(gameObject.name + " entered");
    }
}
