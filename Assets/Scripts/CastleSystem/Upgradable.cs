using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class Upgradable : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject buildingObject;
    [SerializeField] private Sprite startingSprite;
    [SerializeField] private bool isStartingFromNothing;
    [SerializeField] private UpgradeStage[] upgradeStages;
    [SerializeField] private UnityEvent onUpgradeStarted;
    [SerializeField] private UnityEvent onUpgradeEnded;
    private GameObject _building;
    private bool _needRepairing = false;
    private bool _isBroken = false;
    private bool _isProcessing = false;
    private float _survivingPercentage = 100f;
    private int _currentLevel;

    private void Awake()
    {
        if(isStartingFromNothing) _currentLevel = -1;
        else 
        {
            _currentLevel = 0;
            _building = Instantiate(upgradeStages[_currentLevel].Building, transform);
            buildingObject.SetActive(false);
            _building.GetComponent<BreakingHealth>().onStageDowngraded.AddListener(SetToShouldBeRepaired);
        }
    }

    public void Invoke(GameObject player, InteractionUI interactionUI)
    {
        if(_isProcessing) return;
        if(_needRepairing) StartCoroutine(Repair());
        else if(CanUpgade()) StartCoroutine(MoveToNextLevel());
    }

    private bool CanUpgade()
    {
        return _currentLevel < upgradeStages.Length-1;
    }

    public void BreakBuilding()
    {
        Destroy(_building);
        buildingObject.GetComponent<SpriteRenderer>().sprite = upgradeStages[_currentLevel].BuildingStages[0];
        buildingObject.SetActive(true);
        _isBroken = true;
    }

    public void SetToShouldBeRepaired(float percentage)
    {
        _needRepairing = true;
        _survivingPercentage = percentage;
        if(percentage==0) BreakBuilding();
    }

    private IEnumerator Repair()
    {
        _isProcessing = true;
        if(_isBroken == false) BreakBuilding();
        UpgradeStage stage = upgradeStages[_currentLevel];
        int buildingStagesCount = stage.BuildingStages.Length;
        var sprite = buildingObject.GetComponent<SpriteRenderer>();
        float timeToBuildStage = stage.TimeToUpgade/stage.BuildingStages.Length;
        for(int i = 0; i < buildingStagesCount; i++)
        {
            sprite.sprite = stage.BuildingStages[i];
            yield return new WaitForSeconds(timeToBuildStage);
        }
        buildingObject.SetActive(false);
        _building = Instantiate(upgradeStages[_currentLevel].Building, transform);
        _building.GetComponent<BreakingHealth>().onStageDowngraded.AddListener(SetToShouldBeRepaired);
        _needRepairing=false;
        _isBroken = false;
        _isProcessing = false;
    }

    public IEnumerator MoveToNextLevel()
    {
        _isProcessing = true;
        onUpgradeStarted?.Invoke();
        buildingObject.SetActive(true);
        if(_currentLevel >= 0) 
            Destroy(_building);
        UpgradeStage stage = upgradeStages[_currentLevel+1];

        int buildingStagesCount = stage.BuildingStages.Length;
        var sprite = buildingObject.GetComponent<SpriteRenderer>();
        float timeToBuildStage = stage.TimeToUpgade/stage.BuildingStages.Length;
        for(int i = 0; i < buildingStagesCount; i++)
        {
            sprite.sprite = stage.BuildingStages[i];
            yield return new WaitForSeconds(timeToBuildStage);
        }

        _currentLevel++;
        buildingObject.SetActive(false);
        _building = Instantiate(upgradeStages[_currentLevel].Building, transform);
        _building.GetComponent<BreakingHealth>().onStageDowngraded.AddListener(SetToShouldBeRepaired);
        onUpgradeEnded.Invoke();
        _isProcessing = false;
    }

    [Serializable]
    private class UpgradeStage
    {
        public GameObject Building;
        public Sprite[] BuildingStages;
        public float TimeToUpgade;
        public float Price;

    }
}
