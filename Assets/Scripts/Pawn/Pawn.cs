using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Pawn : MonoBehaviour
{
    [SerializeField] private bool isMasterPawn;
    [SerializeField] private PawnState pawnState;
    [SerializeField] private Worker worker;
    [SerializeField] private PawnMovement pawnMovement;
    [SerializeField] private GameObject houseObject;
    private Health _houseObjectHealth;
    private WorkingPoint _currentWork;
    private Health _enemyHealth;

    private void Start()
    {
        pawnState = PawnState.Walking;
        if(!isMasterPawn)
        {
            _houseObjectHealth = houseObject.GetComponentInChildren<Health>();
            _houseObjectHealth.OnBroken.AddListener(HouseBroken);
        }
    }

    public bool CanGetToWork()
    {
        if(pawnState == PawnState.Walking) return true;
        return false;
    }

    public PawnState GetPawnState()
    {
        return pawnState;
    }

    public void AssignWork(WorkingPoint work)
    {
        if(pawnState != PawnState.Walking) return;
        _currentWork = work;
        var workType = work.GetWorkType();
        var endWorkEvent = work.GetOnWorkEndedEvent();
        float workingPlaceOffset = work.GetWorkingPlaceOffset();
        endWorkEvent.AddListener(EndWork);
        endWorkEvent.AddListener(worker.StopWork);
        var workPoint = work.GetWorkingPlace();
        UnityEvent onCameToPoint = pawnMovement.GoToWorkInit(workPoint.transform.position, workingPlaceOffset);
        onCameToPoint.AddListener(CameToWorkPoint);
        pawnState = PawnState.GoingToWork;
    }

    private void EndWork()
    {
        pawnState = PawnState.Walking;
    }

    private void CameToWorkPoint()
    {
        pawnState = PawnState.Working;
        switch(_currentWork.GetWorkType())
        {
            case WorkTypes.Building:
                _currentWork.StartWork(this);
                worker.BuildBuilding(_currentWork.GetWorkingPlace().GetComponent<Upgradable>());
                break;
            case WorkTypes.Chopping:
                _currentWork.StartWork(this);
                worker.Chopping();
                break;
            case WorkTypes.Minining:
                _currentWork.StartWork(this);
                worker.Mine();
                break;
        }
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Enemy"))
        {
            if(isMasterPawn) return;
            _enemyHealth = col.GetComponent<Health>();
            _enemyHealth.OnBroken?.AddListener(OnEnemyDied);
            UnityEvent onCameToHouse = pawnMovement.RunningFromEnemyInit(houseObject.transform.position);
            onCameToHouse?.AddListener(CameToHouse);
            pawnState = PawnState.RunningFromEnemy;
        }
    }

    private void CameToHouse()
    {
        pawnState = PawnState.SittingAtHome;
        gameObject.SetActive(false);
    }

    private void OnEnemyDied()
    {
        pawnState = PawnState.Walking;
        gameObject.SetActive(true);
    }

    private void HouseBroken()
    {
        pawnState = PawnState.Died;
        gameObject.SetActive(false);
    }
}

public enum PawnState
{
    Free, 
    Walking,
    GoingToWork,
    Working, 
    RunningFromEnemy, 
    SittingAtHome, 
    Died
}
