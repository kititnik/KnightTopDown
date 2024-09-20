using UnityEngine;

public class Quest
{
    public QuestInfoSO Info;
    public QuestState State;
    private int currentQuestStepIndex;
    public Quest(QuestInfoSO questInfo)
    {
        Info = questInfo;
        State = QuestState.CanStart;
        currentQuestStepIndex = 0;
    }

    public void MoveToNextStep()
    {
        currentQuestStepIndex++;
    }

    public bool CurrentStepExists()
    {
        return currentQuestStepIndex < Info.QuestStepPrefabs.Length;
    }

    public void InstatiateCurrentQuestStep(Transform parentTransform)
    {
        GameObject questStepPrefab = GetCurrentQuestStepPrefab();
        if (questStepPrefab != null)
        {
            QuestStep questStep = Object.Instantiate(questStepPrefab, parentTransform)
                .GetComponent<QuestStep>();
            questStep.InitializeQuestStep(Info.Id);
        }
    }

    private GameObject GetCurrentQuestStepPrefab()
    {
        GameObject questStepPrefab = null;
        if (CurrentStepExists())
        {
            questStepPrefab = Info.QuestStepPrefabs[currentQuestStepIndex];
        }
        else 
        {
            Debug.LogWarning("Tried to get quest step prefab, but stepIndex was out of range indicating that "
                + "there's no current step: QuestId=" + Info.Id + ", stepIndex=" + currentQuestStepIndex);
        }
        return questStepPrefab;
    }
}
