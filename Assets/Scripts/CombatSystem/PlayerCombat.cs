using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerCombat : MeleeDamager
{
    private Animator _animator;
    private Timer _attackDelayTimer;
    private bool _canAttack = true;
    private float _attackDelay = 0.5f;
    private float _swordDamage = 10;


    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _attackDelayTimer = new Timer(this);

        _attackDelayTimer.TimeIsOver += SetCanAttack; 
    }

    private void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            if(EventSystem.current.IsPointerOverGameObject()) return;
            MeleeAttack();
        }
    }

    private void SetCanAttack() => _canAttack = true;
    
    public void MeleeAttack()
    {
        if (!_canAttack) return;
        _canAttack = false;
        _attackDelayTimer.StartTimer(_attackDelay);
        _animator.Play("Attacking");
    }

    public override float GetMeleeDamage()
    {
        return _swordDamage;
    }
}
