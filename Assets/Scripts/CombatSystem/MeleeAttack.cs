using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MeleeAttack : MeleeDamager
{
    [SerializeField] private UnityEvent onAttackStarted;
    [SerializeField] private UnityEvent onAttackEnded;

    private Timer _attackDelayTimer;
    private float _attackDuration = 0.1f;
    private bool _canAttack = true;
    private float _attackDelay = 1f;
    private float _swordDamage = 10;


    private void Awake()
    {
        _attackDelayTimer = new Timer(this);
        _attackDelayTimer.TimeIsOver += SetCanAttack; 
    }

    private void Update()
    {
        if(_canAttack) StartCoroutine(Attack());
    }

    private void SetCanAttack() => _canAttack = true;
    
    public IEnumerator Attack()
    {
        onAttackStarted?.Invoke();
        yield return new WaitForSeconds(_attackDuration);
        onAttackEnded?.Invoke();
        _canAttack = false;
        _attackDelayTimer.RestartTimer(_attackDelay);
    }

    public override float GetMeleeDamage()
    {
        return _swordDamage;
    }
}
