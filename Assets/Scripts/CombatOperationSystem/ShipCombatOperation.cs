using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipCombatOperation : MonoBehaviour, ICombatOperation
{
    [SerializeField] private GameObject ship;
    [SerializeField] private Vector2[] placesToSpawnShip;
    [SerializeField] private GameObject[] enemies;
    [SerializeField] private int startEnemiesCount;
    [SerializeField] private int startMaxAllowedEnemyExclusive;
    [SerializeField] private float enemiesDifficultyMultiplier;
    [SerializeField] private float enemiesCountMultiplier;
    private Transform landingPoint;
    private int _currentEnemiesCount;
    private int _currentMaxAllowedEnemy;
    private Action _onOperationEnded;
    private float _difficulty;
    private GameObject _shipObj;

    public bool StartOperation(float difficulty, Action onOperationEnded, Transform[] spawnPoints)
    {
        _difficulty = difficulty;
        _onOperationEnded = onOperationEnded;
        landingPoint = spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Length)];
        Vector2 placeToSpawnShip = placesToSpawnShip[UnityEngine.Random.Range(0, placesToSpawnShip.Length)];
        _shipObj = Instantiate(ship, placeToSpawnShip, Quaternion.identity);
        var toPointMovement = _shipObj.GetComponent<ToPointMovement>();
        if(toPointMovement == null) return false;
        toPointMovement.Init(landingPoint.position);
        toPointMovement.OnCameToPoint.AddListener(Landing);
        return true;
    }

    public void Landing()
    {
        _currentEnemiesCount = (int)Mathf.Round(startEnemiesCount*_difficulty*enemiesCountMultiplier);
        _currentMaxAllowedEnemy = (int)Mathf.Round(startMaxAllowedEnemyExclusive*_difficulty*enemiesDifficultyMultiplier);
        for(int i = 0; i < _currentEnemiesCount; i++)
        {
            int enemyIndex = Math.Min(enemies.Length-1, UnityEngine.Random.Range(0, _currentMaxAllowedEnemy-1));
            GameObject go = GameObject.Instantiate(enemies[enemyIndex], landingPoint.position, Quaternion.identity);
            Health health = go.GetComponent<Health>();
            if(health != null) health.OnBroken.AddListener(OnEnemyDied);
        }
    }

    private void OnEnemyDied()
    {
        _currentEnemiesCount--;
        if(_currentEnemiesCount == 0) 
        {
            _onOperationEnded?.Invoke();
            Destroy(_shipObj);
            Destroy(gameObject);
        }
    }
}
