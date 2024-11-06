using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class PawnMovement : MonoBehaviour
{
    [SerializeField] private float _goingToWorkSpeed;
    [SerializeField] private float _walkingSpeed;
    private NavMeshAgent _navMeshAgent;
    private Animator _animator;
    private Pawn _pawn;
    private Vector2 _currentTargetPosition;
    private UnityEvent _onCameToPoint;
    private UnityEvent _onCameToHouse;
    private float _offset;
    private float _timeToChangeRandomDirection = 0;
    private float _timeToStayOnRandomPlace = 0;
    private Vector2 _randomDirection;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _pawn = GetComponent<Pawn>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _navMeshAgent.updateRotation = false;
        _navMeshAgent.updateUpAxis = false; 
        _navMeshAgent.enabled = false;
        StartCoroutine(UpdateNavMeshCo());
    }

    public UnityEvent GoToWorkInit(Vector2 workingPointPos, float workingPlaceOffset)
    {
        _currentTargetPosition = workingPointPos;
        _offset = workingPlaceOffset;
        _animator.Play("Run");
        _navMeshAgent.enabled = true;
        UpdateNavMesh();
        _onCameToPoint = new UnityEvent();
        return _onCameToPoint;
    }

    public UnityEvent RunningFromEnemyInit(Vector2 housePos)
    {
        _currentTargetPosition = housePos;
        _animator.Play("Run");
        _onCameToHouse = new UnityEvent();
        return _onCameToHouse;
    }

    private IEnumerator UpdateNavMeshCo()
    {
        while(gameObject)
        {
            UpdateNavMesh();
            yield return new WaitForSeconds(.3f);
        }
    }
    private void UpdateNavMesh()
    {
        if(_navMeshAgent.enabled)
        {
            _navMeshAgent.SetDestination(_currentTargetPosition);
        }
    }

    private void RunningFromEnemy()
    {
        if(_currentTargetPosition == Vector2.zero) return;
        Vector2 direction = _currentTargetPosition - (Vector2)transform.position;
        direction.Normalize();
        Flip(direction);
        if(Vector2.Distance(transform.position, _currentTargetPosition) <= 1.2f)
        {
            _onCameToHouse?.Invoke();
            _navMeshAgent.enabled = false;
        }
    }

    private void GoingToWork()
    {
        if(_currentTargetPosition == Vector2.zero) return;
        //transform.position = Vector2.MoveTowards(transform.position, _workingPointPos, _goingToWorkSpeed * Time.deltaTime);
        Vector2 direction = _currentTargetPosition - (Vector2)transform.position;
        direction.Normalize();
        Flip(direction);
        if(Vector2.Distance(transform.position, _currentTargetPosition) <= _offset)
        {
            _onCameToPoint?.Invoke();
            _navMeshAgent.enabled = false;
        }
    }

    private void MovingAround()
    {
        if(_timeToStayOnRandomPlace > 0) _timeToStayOnRandomPlace -= Time.deltaTime;
        else if(_timeToStayOnRandomPlace <= 0 && _timeToChangeRandomDirection <= 0)
        {
            _timeToChangeRandomDirection = Random.Range(4, 8);
            int x = Random.Range(-1, 2);
            int y = Random.Range(-1, 2);
            _randomDirection = new Vector2(x, y);
            Flip(_randomDirection);
            _animator.Play("Run");
        }
        else if(_timeToChangeRandomDirection > 0)
        {
            transform.position += (Vector3)_randomDirection * (Time.deltaTime * _walkingSpeed);
            _timeToChangeRandomDirection -= Time.deltaTime;
            if(_timeToChangeRandomDirection <= 0)
            {
                _animator.Play("Idle");
                _timeToStayOnRandomPlace = Random.Range(1, 2);
            }
        }
    }

    private void Flip(Vector2 direction)
    {
        if(direction.x < 0 && transform.localScale.x > 0 ||
            direction.x > 0 && transform.localScale.x < 0) 
        {
            transform.localScale = new Vector3(transform.localScale.x*(-1), transform.localScale.y, transform.localScale.z);
        }
    }

    public void Update()
    {
        if(_pawn.GetPawnState() == PawnState.GoingToWork) GoingToWork();
        //else if(_pawn.GetPawnState() == PawnState.Walking || _pawn.GetPawnState() == PawnState.Free) MovingAround();
        else if(_pawn.GetPawnState() == PawnState.RunningFromEnemy) RunningFromEnemy();
    }
}
