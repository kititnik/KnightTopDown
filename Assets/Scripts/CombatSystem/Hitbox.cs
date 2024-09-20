using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Hitbox : MonoBehaviour
{
    [SerializeField] private UnityEvent OnHitted;
    public UnityEvent<float> PassDamageEvents;
    public UnityEvent<Vector2> PassKnockbackEvents;

    public void Hit(float damage, Vector2 postion)
    {
        PassDamageEvents?.Invoke(damage);
        PassKnockbackEvents?.Invoke(postion);
        OnHitted?.Invoke();
    }
}
