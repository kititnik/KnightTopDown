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
    [SerializeField] private float commonOffset;
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
            if(isMovingByAI && stalkingTarget != null)
            {
                _navMeshAgent.SetDestination(stalkingTarget.position);
            }
            yield return new WaitForSeconds(.3f);
        }
    }

    private void UpdateTarget()
    {
        GameObject go = _movementAI.UpdateTarget(stalkingTags);
        if(go == null) 
        {
            stalkingTarget = null;
            return;
        }
        stalkingTarget = go.transform;
        var approachDistance = go.GetComponent<ApproachDistance>();
        if(approachDistance == null) _navMeshAgent.stoppingDistance = commonOffset;
        else _navMeshAgent.stoppingDistance = approachDistance.GetDistance();
    }

    private IEnumerator UpdateTargetCoroutine()
    {
        while(_movementAI != null)
        {
            UpdateTarget();
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
        bool shouldMove = _canMove && 
                            stalkingTarget != null;
        if(!shouldMove)
        {
            _animator.SetBool("isMoving", false);
            return;
        }
        if(Vector2.Distance(_rigidbody2D.position, _lastPos) < 0.01)
        {
            _animator.SetBool("isMoving", false);
            return;
        }
        Vector2 direction = (Vector2)stalkingTarget.position - _rigidbody2D.position;
        direction.Normalize();
        Flip(direction);
        _animator.SetBool("isMoving", true);
        _animator.SetFloat("moveX", direction.x);
        _animator.SetFloat("moveY", direction.y);
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
