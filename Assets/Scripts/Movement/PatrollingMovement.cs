using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrollingMovement : MonoBehaviour
{
    [SerializeField] private Transform homePosition;
    [SerializeField] private float chasingRadius;
    [SerializeField] private string[] stalkingTags;
    private bool _canMove = true;
    private Vector2 _lastPos;
    private Transform stalkingTarget;
    private Rigidbody2D _rigidbody2D;
    private Animator _animator;
    private NavMeshAgent _navMeshAgent;

    private void Awake()
    {
        StartCoroutine(UpdateTargetCoroutine());
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _navMeshAgent.updateRotation = false;
        _navMeshAgent.updateUpAxis = false;
        StartCoroutine(UpdateNavMesh());
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void UpdateTarget()
    {
        List<Transform> cheasableObjects = new List<Transform>();
        foreach(var targetTag in stalkingTags)
        {
            GameObject[] targets = GameObject.FindGameObjectsWithTag(targetTag);
            foreach(var target in targets)
            {
                cheasableObjects.Add(target.transform);
            }
        }
        float minDistance = Mathf.Infinity;
        Transform nearestTarget = null;
        foreach(var target in cheasableObjects)
        {
            float dist = Vector2.Distance(target.position, transform.position);
            if(dist < minDistance)
            {
                nearestTarget = target;
                minDistance = dist;
            }
        }
        stalkingTarget = nearestTarget;
    }

    private IEnumerator UpdateTargetCoroutine()
    {
        while(true)
        {
            UpdateTarget();
            yield return new WaitForSeconds(5f);
        }
    }

    private IEnumerator UpdateNavMesh()
    {
        while(true)
        {
            if(stalkingTarget != null)
            {
                _navMeshAgent.SetDestination(stalkingTarget.position);
            }
            yield return new WaitForSeconds(.3f);
        }
    }
    
    private void Move()
    {
        if(stalkingTarget == null)
        {
            UpdateTarget();
        }
        float dist = Vector2.Distance(stalkingTarget.position, homePosition.position);
        bool shouldMove = _canMove && 
                            stalkingTarget != null;
        if(!shouldMove)
        {
            _animator.SetBool("isMoving", false);
            return;
        }
        if(dist > chasingRadius)
        {
            stalkingTarget = homePosition;
        }
        Vector2 direction = _rigidbody2D.position - _lastPos;
        direction.Normalize();
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

    private void OnCollisionEnter2D(Collision2D other)
    {
        _navMeshAgent.isStopped = true;
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        _navMeshAgent.isStopped = false;
    }
}
