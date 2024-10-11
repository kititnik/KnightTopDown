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
    public event Action<string, Vector2> OnFinishQuest;
    public void FinishQuest(string id, Vector2 pos)
    {
        OnFinishQuest?.Invoke(id, pos);
    }

    public event Action<Quest> OnQuestStateChange;
    public void QuestStateChange(Quest quest)
    {
        OnQuestStateChange?.Invoke(quest);
    }

}
