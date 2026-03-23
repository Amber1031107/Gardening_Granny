using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [Header("Music Event")]
    public AK.Wwise.Event playSpringMusic;

    [Header("Time Of Day States")]
    public AK.Wwise.State dayState;
    public AK.Wwise.State nightState;

    void Start()
    {
        Debug.Log("MusicManager: START called");

        // 1. FORCE DAY STATE FIRST (critical)
        if (dayState != null)
        {
            dayState.SetValue();
            Debug.Log("MusicManager: Day state set");
        }
        else
        {
            Debug.LogError("MusicManager: Day state NOT assigned");
        }

        // 2. THEN PLAY MUSIC
        if (playSpringMusic != null)
        {
            AkSoundEngine.PostEvent("Play_Spring_MUSIC", gameObject);
            Debug.Log("MusicManager: Spring music event posted");
        }
        else
        {
            Debug.LogError("MusicManager: Music event NOT assigned");
        }
    }

    public void SetNight()
    {
        if (nightState != null)
        {
            nightState.SetValue();
            Debug.Log("MusicManager: Switched to NIGHT");
        }
        else
        {
            Debug.LogError("MusicManager: Night state NOT assigned");
        }
    }

    public void SetDay()
    {
        if (dayState != null)
        {
            dayState.SetValue();
            Debug.Log("MusicManager: Switched to DAY");
        }
        else
        {
            Debug.LogError("MusicManager: Day state NOT assigned");
        }
    }
}