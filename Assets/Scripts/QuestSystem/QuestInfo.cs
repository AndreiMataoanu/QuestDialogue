using UnityEngine;

[CreateAssetMenu(fileName = "QuestInfo", menuName = "Scriptable Objects/QuestInfo", order = 1)]
public class QuestInfo : ScriptableObject
{
    [field: SerializeField] public string id { get; private set; }

    [Header("General")]
    public string displayName;

    [Header("Requirements")]
    public int levelRequirement;

    public QuestInfo[] questPrerequisites;

    [Header("Steps")]
    public GameObject[] questStepPrefabs;

    [Header("Rewards")]
    public int goldReward;
    public int experienceReward;

    //Ensures the ID is always the name of the Scribtable Object asset.
    private void OnValidate()
    {
        #if UNITY_EDITOR
        id = this.name;
        UnityEditor.EditorUtility.SetDirty(this);
        #endif
    }
}
