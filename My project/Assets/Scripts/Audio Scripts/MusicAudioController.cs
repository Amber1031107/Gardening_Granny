using UnityEngine;
using AK.Wwise;

public class MusicAudioController : MonoBehaviour
{
    [Header("Wwise Music Event")]
    public AK.Wwise.Event musicEvent; // Drag your Spring music event

    [Header("Day/Night States")]
    public State dayState;   // Selectable in Inspector
    public State nightState; // Selectable in Inspector

    [Header("Audio Source Reference")]
    public GameObject musicSource; // Usually the Season Audio Manager or child

    // --- Play / Stop Music ---
    public void Play()
    {
        if (musicEvent != null && musicSource != null)
            musicEvent.Post(musicSource);
    }

    public void Stop()
    {
        if (musicEvent != null && musicSource != null)
            musicEvent.Stop(musicSource);
    }

    // --- Day / Night ---
    public void SetDay()
    {
        if (dayState != null)
            dayState.SetValue();
    }

    public void SetNight()
    {
        if (nightState != null)
            nightState.SetValue();
    }
}