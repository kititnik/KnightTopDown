using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCombatOperation : MonoBehaviour, ICombatOperation
{
    [SerializeField] private GameObject enemy;
    public bool StartOperation(float difficulty, Action onOperationEnded, Transform[] spawnPoints)
    {
        Debug.Log("Test operation started");
        int spawnPointIndex = UnityEngine.Random.Range(0, spawnPoints.Length);
        Instantiate(enemy, spawnPoints[spawnPointIndex].transform.position, Quaternion.identity);
        return true;
    }
}