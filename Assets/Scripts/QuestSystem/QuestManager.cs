using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    private Dictionary<string, Quest> questMap;

    private void Awake()
    {
        questMap = CreateQuestMap();
    }

    private void OnEnable()
    {
        EventsManager.instance.QuestEvents.OnStartQuest += StartQuest;
        EventsManager.instance.QuestEvents.OnAdvanceQuest += AdvanceQuest;
        EventsManager.instance.QuestEvents.OnFinishQuest += FinishQuest;
    }

    private void OnDisable()
    {
        EventsManager.instance.QuestEvents.OnStartQuest -= StartQuest;
        EventsManager.instance.QuestEvents.OnAdvanceQuest -= AdvanceQuest;
        EventsManager.instance.QuestEvents.OnFinishQuest -= FinishQuest;
    }

    private void Start()
    {
        foreach(Quest quest in questMap.Values)
        {
            EventsManager.instance.QuestEvents.QuestStateChange(quest);
        }
    }

    private void ChangeQuestState(string id, QuestState newState)
    {
        Quest quest = GetQuestById(id);
        quest.State = newState;
        EventsManager.instance.QuestEvents.QuestStateChange(quest);
    }

    private void StartQuest(string id)
    {
        Quest quest = GetQuestById(id);
        ChangeQuestState(quest.Info.Id, QuestState.InProgress);
        quest.InstatiateCurrentQuestStep(transform);
    }

    private void AdvanceQuest(string id)
    {
        Quest quest = GetQuestById(id);
        quest.MoveToNextStep();
        if(quest.CurrentStepExists())
        {
            quest.InstatiateCurrentQuestStep(transform);
        }
        else
        {
            ChangeQuestState(quest.Info.Id, QuestState.CanFinish);
        }
    }

    private void FinishQuest(string id, GameObject questPoint)
    {
        Quest quest = GetQuestById(id);
        ChangeQuestState(quest.Info.Id, QuestState.Finished);
        quest.Info.Reward.GetFullReward(questPoint.transform.position, questPoint.GetComponent<SpriteRenderer>().sortingOrder);
        Debug.Log("Quest finished");
    }

    private Dictionary<string, Quest> CreateQuestMap()
    {
        QuestInfoSO[] allQuests = Resources.LoadAll<QuestInfoSO>("Quests");
        Dictionary<string, Quest> idToQuestMap = new Dictionary<string, Quest>();
        foreach(QuestInfoSO questInfo in allQuests)
        {
            if(idToQuestMap.ContainsKey(questInfo.Id))
            {
                Debug.LogWarning("Duplicate ID found when createing quest map: " + questInfo.Id);
            }
            idToQuestMap.Add(questInfo.Id, new Quest(questInfo));
        }
        return idToQuestMap;
    }

    private Quest GetQuestById(string id)
    {
        Quest quest = questMap[id];
        if(quest == null)
        {
            Debug.LogError("ID not found in Quest Map: " + id);
        }
        return quest;
    }
}
