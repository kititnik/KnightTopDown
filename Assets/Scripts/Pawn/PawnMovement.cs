using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PawnMovement : MonoBehaviour
{
    [SerializeField] private float _goingToWorkSpeed;
    [SerializeField] private float _walkingSpeed;
    private Animator _animator;
    private Pawn _pawn;
    private Vector2 _workingPointPos;
    private UnityEvent _onCameToPoint;
    private float offset = 1f;
    private float _timeToChangeRandomDirection = 0;
    private float _timeToStayOnRandomPlace = 0;
    private Vector2 _randomDirection;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _pawn = GetComponent<Pawn>();
    }

    public UnityEvent GoToWorkInit(Vector2 workingPointPos)
    {
        _workingPointPos = workingPointPos;
        _animator.Play("Run");
        _onCameToPoint = new UnityEvent();
        return _onCameToPoint;
    }

    private void GoingToWork()
    {
        if(_workingPointPos == null) return;
        transform.position = Vector2.MoveTowards(transform.position, _workingPointPos, _goingToWorkSpeed * Time.deltaTime);
        Vector2 direction = _workingPointPos - (Vector2)transform.position;
        direction.Normalize();
        Flip(direction);
        if(Vector2.Distance(transform.position, _workingPointPos) <= offset)
        {
            _onCameToPoint?.Invoke();
        }
    }

    private void MovingAround()
    {
        if(_timeToStayOnRandomPlace > 0) _timeToStayOnRandomPlace -= Time.deltaTime;
        else if(_timeToStayOnRandomPlace <= 0 && _timeToChangeRandomDirection <= 0)
        {
            _timeToChangeRandomDirection = Random.Range(1, 2);
            int x = Random.Range(-1, 2);
            int y = Random.Range(-1, 2);
            _randomDirection = new Vector2(x, y);
            _randomDirection.Normalize();
            Flip(_randomDirection);
            _animator.Play("Run");
        }
        else if(_timeToChangeRandomDirection > 0)
        {
            transform.position += (Vector3)_randomDirection * Time.deltaTime * _walkingSpeed;
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
        else if(_pawn.GetPawnState() == PawnState.Walking || _pawn.GetPawnState() == PawnState.Free) MovingAround();
    }
}
