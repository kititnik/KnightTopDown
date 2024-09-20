using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCombatOperation : MonoBehaviour, ICombatOperation
{
    [SerializeField] private GameObject enemy;
    public bool StartOperation(Action onOperationEnded, Transform spawnPoint)
    {
        Debug.Log("Test operation started");
        Instantiate(enemy, spawnPoint.transform.position, Quaternion.identity);
        return true;
    }
}