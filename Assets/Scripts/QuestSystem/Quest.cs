using UnityEngine;

public class Quest
{
    public QuestInfo info;
    public QuestState state;

    private int currentQuestStepIndex;

    public Quest(QuestInfo questInfo)
    {
        this.info = questInfo;
        this.state = QuestState.RequirementsNotMet;
        this.currentQuestStepIndex = 0;
    }

    public void MoveToNextStep()
    {
        currentQuestStepIndex++;
    }

    public bool CurrentStepExists()
    {
        return (currentQuestStepIndex < info.questStepPrefabs.Length);
    }

    public void InstantiateCurrentQuestStep(Transform parentTransform)
    {
        GameObject questStepPrefab = GetCurrentQuestStepPrefab();

        if(questStepPrefab != null)
        {
            Object.Instantiate<GameObject>(questStepPrefab, parentTransform);
        }
    }

    private GameObject GetCurrentQuestStepPrefab()
    {
        GameObject questStepPrefab = null;

        if(CurrentStepExists())
        {
            questStepPrefab = info.questStepPrefabs[currentQuestStepIndex];
        }
        else
        {
            Debug.LogWarning("Tried to get quest step prefab, but stepIndex was out of range indicating that there's no current step: QuestId = " + info.id + ", stepIndex = " + currentQuestStepIndex);
        }

        return questStepPrefab;
    }
}
