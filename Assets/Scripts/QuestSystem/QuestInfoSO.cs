using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QuestInfoSO", menuName = "Configurations/QuestInfoSO", order = 1)]
public class QuestInfoSO : ScriptableObject
{
    [field:SerializeField] public string Id {get; private set;}
    public string DisplayName;
    public string Description;
    public QuestInfoSO[] QuestPrerequisites;
    public GameObject[] QuestStepPrefabs;
    public int Reward;

    private void OnValidate()
    {
        #if UNITY_EDITOR
        Id = this.name;
        UnityEditor.EditorUtility.SetDirty(this);
        #endif
    }
}
