using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Pawn : MonoBehaviour
{
    [SerializeField] private PawnState pawnState;
    [SerializeField] private Worker worker;
    [SerializeField] private PawnMovement pawnMovement;
    private IWorkingPoint _currentWork;

    public bool CanGetToWork()
    {
        if(pawnState == PawnState.Walking) return true;
        return false;
    }

    public PawnState GetPawnState()
    {
        return pawnState;
    }

    public void AssignWork(IWorkingPoint work)
    {
        _currentWork = work;
        var workType = work.GetWorkType();
        var endWorkEvent = work.GetOnWorkEndedEvent();
        endWorkEvent.AddListener(EndWork);
        endWorkEvent.AddListener(worker.StopWork);
        var workPoint = work.GetWorkingPlace();
        UnityEvent onCameToPoint = pawnMovement.GoToWorkInit(workPoint.transform.position);
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
                _currentWork.StartWork();
                worker.BuildBuilding(_currentWork.GetWorkingPlace().GetComponent<Upgradable>());
                break;
            case WorkTypes.Chopping:
                _currentWork.StartWork();
                worker.Chopping();
                break;
        }
    }
}

public enum PawnState
{
    Free, 
    Walking,
    GoingToWork,
    Working, 
    RunningFromEnemy
}
