using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnManager : MonoBehaviour
{
    [SerializeField] private Pawn[] pawns;

    public void FindWorker(WorkingPoint work)
    {
        foreach(var pawn in pawns)
        {
            if(pawn.GetPawnState() != PawnState.Walking) continue;
            pawn.AssignWork(work);
            return;
        }
    }
}
