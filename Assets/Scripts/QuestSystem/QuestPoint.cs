using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class QuestPoint : MonoBehaviour, IInteractable
{
    [SerializeField] private QuestInfoSO questInfoForPont;
    [SerializeField] private bool startPoint;
    [SerializeField] private bool finishPoint;
    private string questId;
    private QuestState currentQuestState;

    private void Awake()
    {
        questId = questInfoForPont.Id;
        
    }

    private void OnEnable()
    {
        EventsManager.instance.QuestEvents.OnQuestStateChange += QuestStateChange;
    }
    private void OnDisable()
    {
        EventsManager.instance.QuestEvents.OnQuestStateChange -= QuestStateChange;
    }

    private void QuestStateChange(Quest quest)
    {
        if(quest.Info.Id == questId)
        {
            currentQuestState = quest.State;
        }
    }

    public void Invoke(GameObject player, InteractionUI interactionUI)
    {
        if(currentQuestState == QuestState.CanStart && startPoint)
        {
            interactionUI.SetText(interactionUI.RichTextOnTop, questInfoForPont.Description, 5f);
            EventsManager.instance.QuestEvents.StartQuest(questId);
        }
        else if(currentQuestState == QuestState.CanFinish && finishPoint)
        {
            interactionUI.SetText(interactionUI.TextOnTop, "Quest finished: " + questInfoForPont.DisplayName, 3f);
            EventsManager.instance.QuestEvents.FinishQuest(questId, transform.position);
        }
    }

    public void OnNearObject(GameObject player, InteractionUI interactionUI)
    {
        interactionUI.SetText(interactionUI.TextOnTop, questInfoForPont.DisplayName);
    }

    public void OnExitedObject(GameObject player, InteractionUI interactionUI)
    {
        interactionUI.RemoveText(interactionUI.TextOnTop, questInfoForPont.DisplayName);
    }
}
