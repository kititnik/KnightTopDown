using UnityEngine;

public class EventsManager : MonoBehaviour
{
    public static EventsManager instance { get; private set; }

    public InventoryEvents InventoryEvents;
    public QuestEvents QuestEvents;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one Game Events Manager in the scene.");
        }
        instance = this;

        // initialize all events
        InventoryEvents = new InventoryEvents();
        QuestEvents = new QuestEvents();
    }

}
