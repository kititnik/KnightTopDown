using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class QuestStep : MonoBehaviour
{
    private bool isFinished = false;
    private string questId;

    public void InitializeQuestStep(string questId)
    {
        this.questId = questId;
        Initialize();
    }

    public abstract void Initialize();

    protected void FinishQuestStep()
    {
        if(!isFinished)
        {
            isFinished = true;
            EventsManager.instance.QuestEvents.AdvanceQuest(questId);
            Destroy(gameObject);
        }
    }
}
