using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class WorkingPoint : MonoBehaviour, IInteractable
{
    [SerializeField] protected float offset;
    [SerializeField] protected WorkTypes workType;
    [SerializeField] protected Price price;
    [SerializeField] protected UnityEvent onWorkEnded;
    protected InventoryHandler _playerInventoryHandler;

    protected virtual void Start()
    {
        if(onWorkEnded.GetPersistentEventCount() == 0) onWorkEnded = new UnityEvent();
    }

    public GameObject GetWorkingPlace()
    {
        return gameObject;
    }

    public float GetWorkingPlaceOffset()
    {
        return offset;
    }

    public WorkTypes GetWorkType()
    {
        return workType;
    }

    public UnityEvent GetOnWorkEndedEvent()
    {
        return onWorkEnded;
    }

    public virtual void StartWork(Pawn pawn)
    {
        if(!price.TryTakePrice(_playerInventoryHandler)) return;
    }

    public virtual void Invoke(GameObject player, InteractionUI interactionUI)
    {
        _playerInventoryHandler = player.GetComponent<InventoryHandler>();
        if (!price.CheckPayingAbility(_playerInventoryHandler))
        {
            return;
        }
    }

    public virtual void OnNearObject(GameObject player, InteractionUI interactionUI)
    {
        interactionUI.SetText(interactionUI.TextOnTop, gameObject.name);
        interactionUI.SetText(interactionUI.RichTextOnTop, price.ToString());
    }

    public virtual void OnExitedObject(GameObject player, InteractionUI interactionUI)
    {
        interactionUI.RemoveText(interactionUI.TextOnTop, gameObject.name);
        interactionUI.RemoveText(interactionUI.RichTextOnTop, price.ToString());
    }
}
