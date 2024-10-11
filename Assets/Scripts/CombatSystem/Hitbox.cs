using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Hitbox : MonoBehaviour
{
    [SerializeField] private UnityEvent OnHitted;
    public UnityEvent<float> PassDamageEvents;
    public UnityEvent<Vector2> PassKnockbackEvents;

    public void Hit(float damage, Vector2 position)
    {
        // StartCoroutine(HitCor(damage, position));
        PassDamageEvents?.Invoke(damage);
        PassKnockbackEvents?.Invoke(position);
        OnHitted?.Invoke();
    }

    // private IEnumerator HitCor(float damage, Vector2 position)
    // {
    //     yield return new WaitForSeconds(0.3f);
    //     PassDamageEvents?.Invoke(damage);
    //     PassKnockbackEvents?.Invoke(position);
    //     OnHitted?.Invoke();
    // }
}
