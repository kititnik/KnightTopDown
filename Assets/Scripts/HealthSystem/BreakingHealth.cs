using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BreakingHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private float maxHealth;
    [SerializeField] private float damageResistance;
    [SerializeField] List<DamageStage> damageStages;
    public UnityEvent onBroken;
    public UnityEvent<float> onStageDowngraded;
    private int _currentDamageStage;
    private float _health;
    private Collider2D _collider;

    private void Awake()
    {
        _health = maxHealth;
        _collider = GetComponent<Collider2D>();
    }

    public void GetDamage(float damage)
    {
        if(_health <= 0) return;
        _health -= damage - damageResistance;
        //Debug.Log(gameObject.name + " got damage: " + damage +  ". Current health: " + _health);
        if(_health*100.0/maxHealth <= damageStages[_currentDamageStage].Percentage)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = damageStages[_currentDamageStage].Sprite;
            onStageDowngraded?.Invoke(damageStages[_currentDamageStage].Percentage);
            _currentDamageStage++;
        }
        if(_health <= 0) Death();
    }

    public void Death()
    {
        onBroken?.Invoke();
    }

    [Serializable]
    class DamageStage
    {
        public float Percentage;
        public Sprite Sprite;
    }
}
