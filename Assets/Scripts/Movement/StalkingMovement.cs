using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using NavMeshPlus.Components;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class StalkingMovement : MonoBehaviour
{
    [SerializeField] private string[] stalkingTags;
    [SerializeField] private float speed;
    private MovementAI _movementAI;
    private Transform stalkingTarget;
    private bool _canMove = true;
    private Vector2 _lastPos;
    private Rigidbody2D _rigidbody2D;
    private Animator _animator;
    private NavMeshAgent _navMeshAgent;
    private bool isMovingByAI = true;
    private Vector2 moveToPos;

    private void Awake()
    {
        _movementAI = GetComponent<MovementAI>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _navMeshAgent.updateRotation = false;
        _navMeshAgent.updateUpAxis = false;
        StartCoroutine(UpdateTargetCoroutine());
        StartCoroutine(UpdateNavMesh());
    }

    public void StopMovement() => _canMove = false;
    public void ResumeMovement() => _canMove = true;

    private void FixedUpdate()
    {
        if(isMovingByAI) MoveWithNavMesh();
        else MoveToPosition(moveToPos);
    }

    public void SetObjectToGoToPosition(Vector2 pos)
    {
        _navMeshAgent.enabled = false;
        isMovingByAI = false;
        moveToPos = pos;
    }

    private IEnumerator UpdateNavMesh()
    {
        while(gameObject != null)
        {
            if(isMovingByAI)
            {
                _navMeshAgent.SetDestination(stalkingTarget.position);
            }
            yield return new WaitForSeconds(.3f);
        }
    }

    private IEnumerator UpdateTargetCoroutine()
    {
        while(_movementAI != null)
        {
            stalkingTarget = _movementAI.UpdateTarget(stalkingTags);
            yield return new WaitForSeconds(5f);
        }
    }

    private void MoveToPosition(Vector2 position)
    {
        var pos = Vector2.MoveTowards(transform.position, position, speed * Time.fixedDeltaTime);
        _rigidbody2D.MovePosition(pos);
        if(Vector2.Distance(transform.position, position) < 0.01)
        {
            _navMeshAgent.enabled = true;
            isMovingByAI = true;
        }
    }

    private void MoveWithNavMesh()
    {
        if(stalkingTarget == null)
        {
            stalkingTarget = _movementAI.UpdateTarget(stalkingTags);
            UpdateNavMesh();
        }
        bool shouldMove = _canMove && 
                            stalkingTarget != null;
        if(!shouldMove)
        {
            _animator.SetBool("isMoving", false);
            return;
        }
        Vector2 direction = _rigidbody2D.position - _lastPos;
        direction.Normalize();
        Flip(direction);
        if(direction != Vector2.zero)
        {
            _animator.SetBool("isMoving", true);
            _animator.SetFloat("moveX", direction.x);
            _animator.SetFloat("moveY", direction.y);
        }
        else
        {
            _animator.SetBool("isMoving", false);
            Vector2 toTargetDirection = (Vector2)stalkingTarget.position - _rigidbody2D.position;
            direction.Normalize();
            _animator.SetFloat("moveX", toTargetDirection.x);
            _animator.SetFloat("moveY", toTargetDirection.y);
        }
        _lastPos = transform.position;        
    }

    private void Flip(Vector2 direction)
    {
        if(direction.x > 0 && transform.localScale.x < 0 ||
            direction.x < 0 && transform.localScale.x > 0) 
        {
            transform.localScale = new Vector3(transform.localScale.x*-1, transform.localScale.y, transform.localScale.z);
        }
    }
}
