using UnityEngine;
using AK.Wwise;

public class SeasonAmbienceController : MonoBehaviour
{
    [Header("Ambience Event")]
    public AK.Wwise.Event seasonAmbienceEvent;

    [Header("Time Of Day States")]
    public AK.Wwise.State dayState;
    public AK.Wwise.State nightState;

    void Start()
    {
        if (seasonAmbienceEvent != null)
        {
            seasonAmbienceEvent.Post(gameObject);
        }

        // Default start as Day
        SetDay();
    }

    public void SetDay()
    {
        dayState.SetValue();
        Debug.Log("Time set to DAY");
    }

    public void SetNight()
    {
        nightState.SetValue();
        Debug.Log("Time set to NIGHT");
    }
}