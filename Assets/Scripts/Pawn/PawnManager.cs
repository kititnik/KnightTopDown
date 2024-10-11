using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnManager : MonoBehaviour
{
    [SerializeField] private Pawn[] pawns;

    private void Start()
    {
        pawns = FindObjectsOfType<Pawn>();
    }

    public void FindWorker(IWorkingPoint work)
    {
        pawns[0].AssignWork(work);
    }
}
