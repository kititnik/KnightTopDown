using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class TreeChoppingPlace : MonoBehaviour, IInteractable, IWorkingPoint
{
    [SerializeField] private float damage;
    [SerializeField] private float intervalBetweenHits;
    private PawnManager _pawnManager;
    private UnityEvent _onWorkEnded;
    private Health _treeHealth;
    private Hitbox _hitBox;

    private void Start()
    {
        _pawnManager = FindObjectOfType<PawnManager>();
        _onWorkEnded = new UnityEvent();
        _hitBox = GetComponentInChildren<Hitbox>();
        _treeHealth = GetComponent<Health>();
    }

    public UnityEvent GetOnWorkEndedEvent()
    {
        return _onWorkEnded;
    }

    public GameObject GetWorkingPlace()
    {
        return gameObject;
    }

    public WorkTypes GetWorkType()
    {
        return WorkTypes.Chopping;
    }

    public void Invoke(GameObject player, InteractionUI interactionUI)
    {
        _pawnManager.FindWorker(this);
    }

    public void OnExitedObject(GameObject player, InteractionUI interactionUI)
    {
        interactionUI.RemoveText(interactionUI.RichTextOnTop, "Tree");
    }

    public void OnNearObject(GameObject player, InteractionUI interactionUI)
    {
        interactionUI.SetText(interactionUI.RichTextOnTop, "Tree");
    }

    public void StartWork()
    {
        _treeHealth.OnBroken?.AddListener(EndWork);
        StartCoroutine(WorkingCo());
    }

    private IEnumerator WorkingCo()
    {
        while(true)
        {
            yield return new WaitForSeconds(intervalBetweenHits);
            _hitBox.Hit(damage, transform.position);
        }
    }

    private void EndWork()
    {
        StopCoroutine(WorkingCo());
        _onWorkEnded?.Invoke();
    }
}
