using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatOperationManager : MonoBehaviour
{
    [SerializeField] private GameObject[] combatOperations;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private float minDelayBetweenOperations;
    [SerializeField] private float maxDelayBetweenOperations;
    [SerializeField] private float maxDifficulty;
    [SerializeField] private float difficultyMultiplier;
    private float currentDifficulty=1;

    private void Start()
    {
        StartCoroutine(StartOperationCycle());
    }

    private IEnumerator StartOperationCycle()
    {
        while(true)
        {
            float delay = UnityEngine.Random.Range(minDelayBetweenOperations, maxDelayBetweenOperations);
            yield return new WaitForSeconds(delay);
            int currentOperationIndex = UnityEngine.Random.Range(0, combatOperations.Length);
            GameObject operation = Instantiate(combatOperations[currentOperationIndex], gameObject.transform);
            operation.GetComponent<ICombatOperation>().StartOperation(currentDifficulty, OnOperationEnded, spawnPoints);
        }
    }

    private void OnOperationEnded()
    {
        currentDifficulty = Math.Min(maxDifficulty, currentDifficulty*difficultyMultiplier);
    }
}
