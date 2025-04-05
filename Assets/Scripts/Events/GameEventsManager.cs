using UnityEngine;

public class GameEventsManager : MonoBehaviour
{
    public static GameEventsManager instance {get; private set;}

    public MiscEvents miscEvents;
    public GoldEvents goldEvents;

    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogError("Found more than one Game Events Manager in the scene.");
        }

        instance = this;

        miscEvents = new MiscEvents();
        goldEvents = new GoldEvents();
    }
}
