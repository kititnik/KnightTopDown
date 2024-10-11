using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralOperation : MonoBehaviour, ICombatOperation
{
    [SerializeField] private GameObject[] enemies;
    [SerializeField] private int startEnemiesCount;
    [SerializeField] private int startMaxAllowedEnemyExclusive;
    [SerializeField] private float enemiesDifficultyMultiplier;
    [SerializeField] private float enemiesCountMultiplier;
    private int _currentEnemiesCount;
    private int _currentMaxAllowedEnemy;
    private Action _onOperationEnded;
    public bool StartOperation(float difficulty, Action onOperationEnded, Transform[] spawnPoints)
    {
        _onOperationEnded = onOperationEnded;
        _currentEnemiesCount = (int)Mathf.Round(startEnemiesCount*difficulty*enemiesCountMultiplier);
        _currentMaxAllowedEnemy = (int)Mathf.Round(startMaxAllowedEnemyExclusive*difficulty*enemiesDifficultyMultiplier);
        for(int i = 0; i < _currentEnemiesCount; i++)
        {
            int spawnPointIndex = UnityEngine.Random.Range(0, spawnPoints.Length);
            int enemyIndex = Math.Min(enemies.Length-1, UnityEngine.Random.Range(0, _currentMaxAllowedEnemy-1));
            GameObject go = GameObject.Instantiate(enemies[enemyIndex], spawnPoints[spawnPointIndex].position, Quaternion.identity);
            Health health = go.GetComponent<Health>();
            if(health != null) health.OnBroken.AddListener(OnEnemyDied);
        }
        return true;
    }

    private void OnEnemyDied()
    {
        _currentEnemiesCount--;
        if(_currentEnemiesCount == 0) 
        {
            _onOperationEnded?.Invoke();
            Destroy(gameObject);
        }
    }
}
