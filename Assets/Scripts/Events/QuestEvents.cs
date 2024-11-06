using System;
using UnityEngine;

public class QuestEvents
{
    public event Action<string> OnStartQuest;
    public void StartQuest(string id)
    {
        OnStartQuest?.Invoke(id);
    }
    public event Action<string> OnAdvanceQuest;
    public void AdvanceQuest(string id)
    {
        OnAdvanceQuest?.Invoke(id);
    }
    public event Action<string, GameObject> OnFinishQuest;
    public void FinishQuest(string id, GameObject questPoint)
    {
        OnFinishQuest?.Invoke(id, questPoint);
    }

    public event Action<Quest> OnQuestStateChange;
    public void QuestStateChange(Quest quest)
    {
        OnQuestStateChange?.Invoke(quest);
    }

}
