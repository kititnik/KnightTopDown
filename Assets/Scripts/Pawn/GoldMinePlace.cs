using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class GoldMinePlace : MonoBehaviour, IWorkingPoint, IInteractable
{
    [SerializeField] private float offset;
    [SerializeField] private Sprite activeSprite;
    [SerializeField] private Sprite inactiveSprite;
    [SerializeField] private Sprite destroyedSprite;
    [SerializeField] private int startingMineRunCount;
    [SerializeField] private float miningTimeInSeconds;
    [SerializeField] private Reward rewardAtATime;
    private int _currentMineRunCount;
    private UnityEvent _onWorkEnded;
    private PawnManager _pawnManager;
    private SpriteRenderer _spriteRenderer;

    private void Start()
    {   
        _currentMineRunCount = startingMineRunCount;
        _pawnManager = FindObjectOfType<PawnManager>();
        _onWorkEnded = new UnityEvent();
        _spriteRenderer = GetComponent<SpriteRenderer>();
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
        return WorkTypes.Minining;
    }

    public UnityEvent GetOnWorkEndedEvent()
    {
        return _onWorkEnded;
    }

    public void StartWork(Pawn pawn)
    {
        _spriteRenderer.sprite = activeSprite;
        _currentMineRunCount--;
        StartCoroutine(StartWorkCo());
    }

    private IEnumerator StartWorkCo()
    {
        yield return new WaitForSeconds(miningTimeInSeconds);
        _spriteRenderer.sprite = inactiveSprite;
        rewardAtATime.GetFullReward((transform.position + Vector3.down), _spriteRenderer.sortingOrder);
        if(_currentMineRunCount <= 0) _spriteRenderer.sprite = destroyedSprite;
        _onWorkEnded?.Invoke();
    }

    public void Invoke(GameObject player, InteractionUI interactionUI)
    {
        if (_currentMineRunCount <= 0)
        {
            interactionUI.SetText(interactionUI.TextOnTop, "No gold available");
            return;
        }
        _pawnManager.FindWorker(this);
    }

    public void OnNearObject(GameObject player, InteractionUI interactionUI)
    {
        interactionUI.SetText(interactionUI.RichTextOnTop, "Gold mine");
    }

    public void OnExitedObject(GameObject player, InteractionUI interactionUI)
    {
        interactionUI.RemoveText(interactionUI.RichTextOnTop, "Gold mine");
    }
}
