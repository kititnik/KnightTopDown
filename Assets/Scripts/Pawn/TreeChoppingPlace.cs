using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class TreeChoppingPlace : WorkingPoint 
{
    [SerializeField] private float damage;
    [SerializeField] private float intervalBetweenHits;
    private PawnManager _pawnManager;
    private Health _treeHealth;
    private Hitbox _hitBox;

    protected void Start()
    {
        base.Start();
        _pawnManager = FindObjectOfType<PawnManager>();
        _hitBox = GetComponentInChildren<Hitbox>();
        _treeHealth = GetComponent<Health>();
    }

    public override void Invoke(GameObject player, InteractionUI interactionUI)
    {
        base.Invoke(player, interactionUI);
        _pawnManager.FindWorker(this);
    }

    public override void StartWork(Pawn pawn)
    {
        base.StartWork(pawn);
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
        onWorkEnded?.Invoke();
    }
}
