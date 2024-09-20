using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
public class Knockbackable : MonoBehaviour
{
    private Rigidbody2D _rigidBody2D;
    [SerializeField] private float thrust;
    [SerializeField] private float knockbackTime;
    [SerializeField] private UnityEvent OnBegin, OnDone;

    private void Awake()
    {
        _rigidBody2D = GetComponent<Rigidbody2D>();
    }

    public void Knockback(Vector2 kickerPos)
    {
        OnBegin?.Invoke();
        Vector2 difference = _rigidBody2D.position - kickerPos;
        difference = difference.normalized * thrust;
        _rigidBody2D.velocity = difference;
        StartCoroutine(KnockbackCo());
    }

    private IEnumerator KnockbackCo()
    {
        yield return new WaitForSeconds(knockbackTime);
        _rigidBody2D.velocity = Vector2.zero;
        OnDone?.Invoke();
    }
}
