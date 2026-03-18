using UnityEngine;
using AK.Wwise;

public class MusicAudioController : MonoBehaviour
{
    [Header("Wwise Music Events")]
    public AK.Wwise.Event dayMusicEvent;   // Assign your Day music event
    public AK.Wwise.Event nightMusicEvent; // Assign your Night music event

    [Header("Day/Night States (Optional)")]
    public State dayState;   // Adjusts volume/filter/etc.
    public State nightState;

    [Header("Audio Source Reference")]
    public GameObject musicSource; // Usually the Season Audio Manager or a child

    private bool isPlaying = false;

    // --- Play Day music ---
    public void PlayDay()
    {
        StopMusic();            // Stop any previous music
        dayState?.SetValue();   // Optional: adjust Day state
        dayMusicEvent?.Post(musicSource);
        isPlaying = true;
    }

    // --- Play Night music ---
    public void PlayNight()
    {
        StopMusic();             // Stop any previous music
        nightState?.SetValue();  // Optional: adjust Night state
        nightMusicEvent?.Post(musicSource);
        isPlaying = true;
    }

    // --- Stop music ---
    public void StopMusic()
    {
        if (!isPlaying) return;

        if (dayMusicEvent != null)
            dayMusicEvent.Stop(musicSource);
        if (nightMusicEvent != null)
            nightMusicEvent.Stop(musicSource);

        isPlaying = false;
    }
}