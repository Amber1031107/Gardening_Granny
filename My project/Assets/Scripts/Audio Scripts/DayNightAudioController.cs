using UnityEngine;

public class DayNightAudioController : MonoBehaviour
{
    public AK.Wwise.Event playAmbience;
    public AK.Wwise.State dayState;
    public AK.Wwise.State nightState;
    public AK.Wwise.RTPC ambienceMenuMute;

    void Start()
    {
        playAmbience.Post(gameObject);
        ambienceMenuMute.SetGlobalValue(0f);
        SetDay();
    }

    public void SetDay()
    {
        dayState.SetValue();
        Debug.Log("Audio DAY");
    }

    public void SetNight()
    {
        nightState.SetValue();
        Debug.Log("Audio NIGHT");
    }

    public void MuteAmbienceForMenu()
    {
        ambienceMenuMute.SetGlobalValue(1f);
        Debug.Log("Ambience muted for menu");
    }

    public void RestoreAmbienceAfterMenu()
    {
        ambienceMenuMute.SetGlobalValue(0f);
        Debug.Log("Ambience restored after menu");
    }
}