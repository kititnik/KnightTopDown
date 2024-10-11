using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class Upgradable : MonoBehaviour, IInteractable, IWorkingPoint
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
    private PawnManager _pawnManager;
    private ConstructionTypes _constructionType;

    private void Awake()
    {
        _pawnManager = FindObjectOfType<PawnManager>();
        if(isStartingFromNothing) _currentLevel = -1;
        else 
        {
            _currentLevel = 0;
            _building = Instantiate(upgradeStages[_currentLevel].Building, transform);
            buildingObject.SetActive(false);
            _building.GetComponent<BreakingHealth>().onStageDowngraded.AddListener(SetToShouldBeRepaired);
        }
    }

    public void StartWork()
    {
        if(_constructionType == ConstructionTypes.Repair)
        {
            StartCoroutine(Repair());
        }
        else if(_constructionType == ConstructionTypes.Upgrade)
        {
            StartCoroutine(MoveToNextLevel());
        }
    }

    public GameObject GetWorkingPlace()
    {
        return gameObject;
    }

    public WorkTypes GetWorkType()
    {
        return WorkTypes.Building;
    }

    public UnityEvent GetOnWorkEndedEvent()
    {
        return onUpgradeEnded;
    }

    public void Invoke(GameObject player, InteractionUI interactionUI)
    {
        if(_isProcessing) return;
        var inventoryHandler = player.GetComponent<InventoryHandler>();
        if(inventoryHandler == null)
        {
            Debug.LogError("No InventoryHandler on player");
            return;
        }
        if(_needRepairing)
        {
            var currentPrice = upgradeStages[_currentLevel].Price;
            bool isAble = currentPrice.CheckPayingAbility(inventoryHandler);
            if(isAble) 
            {
                currentPrice.TryTakePrice(inventoryHandler);
                _constructionType = ConstructionTypes.Repair;
                _pawnManager.FindWorker(this);
            }
            else interactionUI.AddNoification("Not enought resources", 3f);
        }
        else if(CanUpgade())
        {
            var currentPrice = upgradeStages[_currentLevel+1].Price;
            bool isAble = currentPrice.CheckPayingAbility(inventoryHandler);
            if(isAble) 
            {
                currentPrice.TryTakePrice(inventoryHandler);
                _constructionType = ConstructionTypes.Upgrade;
                _pawnManager.FindWorker(this);
            }
            else interactionUI.AddNoification("Not enought resources", 3f);
        }
        else
        {
            interactionUI.AddNoification("Nothing to upgrade", 3f);
        }
    }

    public void OnExitedObject(GameObject player, InteractionUI interactionUI)
    {
        interactionUI.RemoveText(interactionUI.TextOnTop, gameObject.name);
        if(_needRepairing)
        {
            var currentPrice = upgradeStages[_currentLevel].Price;
            interactionUI.RemoveText(interactionUI.RichTextOnTop, currentPrice.ToString());
        }
        else if(CanUpgade())
        {
            var currentPrice = upgradeStages[_currentLevel+1].Price;
            interactionUI.RemoveText(interactionUI.RichTextOnTop, currentPrice.ToString());
        }
    }

    public void OnNearObject(GameObject player, InteractionUI interactionUI)
    {
        interactionUI.SetText(interactionUI.TextOnTop, gameObject.name);
        if(_needRepairing)
        {
            var currentPrice = upgradeStages[_currentLevel].Price;
            interactionUI.SetText(interactionUI.RichTextOnTop, currentPrice.ToString());
        }
        else if(CanUpgade())
        {
            var currentPrice = upgradeStages[_currentLevel+1].Price;
            interactionUI.SetText(interactionUI.RichTextOnTop, currentPrice.ToString());
        }
    }

    private bool CanUpgade()
    {
        return _currentLevel < upgradeStages.Length-1;
    }

    public void BreakBuilding()
    {
        Destroy(_building);
        buildingObject.GetComponent<SpriteRenderer>().sprite = startingSprite;
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
        public Price Price;

    }

    enum ConstructionTypes
    {
        Repair, 
        Upgrade
    }
}
