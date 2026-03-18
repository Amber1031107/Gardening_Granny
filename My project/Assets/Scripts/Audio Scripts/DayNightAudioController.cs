using UnityEngine;

public class DayNightAudioController : MonoBehaviour
{
    public AK.Wwise.Event playAmbience; // or playMusic
    public AK.Wwise.State dayState;
    public AK.Wwise.State nightState;

    void Start()
    {
        // Start audio FIRST
        playAmbience.Post(gameObject);

        // Then set day
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
}