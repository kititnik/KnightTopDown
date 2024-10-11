using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MeleeAttack : MeleeDamager
{
    [SerializeField] private UnityEvent onAttack;

    private Animator _animator;
    private Timer _attackDelayTimer;
    private float _attackDuration = 0.1f;
    private bool _canAttack = true;
    private float _attackDelay = 2f;
    private float _swordDamage = 10;


    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _attackDelayTimer = new Timer(this);
        _attackDelayTimer.TimeIsOver += SetCanAttack; 
    }

    private void SetCanAttack() => _canAttack = true;
    
    public void Attack()
    {
        onAttack?.Invoke();
        _animator.Play("Attacking");
        _canAttack = false;
        _attackDelayTimer.RestartTimer(_attackDelay);
    }

    public override float GetMeleeDamage()
    {
        return _swordDamage;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(!_canAttack) return;
        if(col.GetComponentInChildren<Hitbox>() == null) return;
        Attack();
    }
}
