using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class GoldMinePlace : WorkingPoint
{
    [SerializeField] private Sprite activeSprite;
    [SerializeField] private Sprite inactiveSprite;
    [SerializeField] private Sprite destroyedSprite;
    [SerializeField] private int startingMineRunCount;
    [SerializeField] private float miningTimeInSeconds;
    [SerializeField] private Reward rewardAtATime;
    private int _currentMineRunCount;
    private PawnManager _pawnManager;
    private SpriteRenderer _spriteRenderer;

    protected override void Start()
    {   
        base.Start();
        _currentMineRunCount = startingMineRunCount;
        _pawnManager = FindObjectOfType<PawnManager>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public override void StartWork(Pawn pawn)
    {
        base.StartWork(pawn);
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
        onWorkEnded?.Invoke();
    }

    public override void Invoke(GameObject player, InteractionUI interactionUI)
    {
        base.Invoke(player, interactionUI);
        if (_currentMineRunCount <= 0)
        {
            interactionUI.SetText(interactionUI.TextOnTop, "No gold available");
            return;
        }
        _pawnManager.FindWorker(this);
    }
}
