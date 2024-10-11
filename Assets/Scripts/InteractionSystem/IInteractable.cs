using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    void Invoke(GameObject player, InteractionUI interactionUI);
    void OnNearObject(GameObject player, InteractionUI interactionUI);
    void OnExitedObject(GameObject player, InteractionUI interactionUI);
}
