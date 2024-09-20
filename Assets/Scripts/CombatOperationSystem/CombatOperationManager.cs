using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatOperationManager : MonoBehaviour
{
    [SerializeField] private GameObject[] combatOperations;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private float minDelayBetweenOperations;
    [SerializeField] private float maxDelayBetweenOperations;
    [SerializeField] private bool isActiveOperation = false;

    private void Start()
    {
        StartCoroutine(StartOperationCycle());
    }

    private IEnumerator StartOperationCycle()
    {
        while(true)
        {
            float delay = Random.Range(minDelayBetweenOperations, maxDelayBetweenOperations);
            yield return new WaitForSeconds(delay);
            int currentOperationIndex = Random.Range(0, combatOperations.Length);
            int spawnPointIndex = Random.Range(0, spawnPoints.Length);
            GameObject operation = Instantiate(combatOperations[currentOperationIndex], gameObject.transform);
            operation.GetComponent<ICombatOperation>().StartOperation(OnOperationEnded, spawnPoints[spawnPointIndex]);
        }
    }

    private void OnOperationEnded()
    {
        isActiveOperation = false;
    }
}
