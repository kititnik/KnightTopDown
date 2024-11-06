using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class ToPointMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Rigidbody2D rigidBody2D;
    [SerializeField] private Animator animator;
    [SerializeField] private NavMeshAgent _navMeshAgent;
    public UnityEvent OnCameToPoint;
    private Vector2 _currentPoint;
    private Vector2 _lastPos = Vector2.positiveInfinity;
    private bool _isOnPoint;
    private bool _isVectorZero;
    private Vector2 _direction;

    public void Init(Vector2 point)
    {
        _currentPoint = point;
        _navMeshAgent.updateRotation = false;
        _navMeshAgent.updateUpAxis = false;
        _navMeshAgent.SetDestination(_currentPoint);
    }

    private void FixedUpdate()
    {
        if(_isOnPoint) return;
        _direction = (rigidBody2D.position - _lastPos).normalized;
        animator.SetFloat("moveX", _direction.x);
        animator.SetFloat("moveY", _direction.y);
        if(_direction == Vector2.zero && !_isVectorZero)
        {
            _isVectorZero = true;
            StopCoroutine(CheckVectorZero());
            StartCoroutine(CheckVectorZero());
        }
        _lastPos = rigidBody2D.transform.position;
    }

    private IEnumerator CheckVectorZero()
    {
        yield return new WaitForSeconds(1f);
        if(_direction == Vector2.zero) 
        {
            _isOnPoint = true;
            OnCameToPoint?.Invoke();
        }
        else _isVectorZero = false;
    }
}
