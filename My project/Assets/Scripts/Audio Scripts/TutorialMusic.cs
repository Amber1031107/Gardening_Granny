using UnityEngine;

public class TutorialMusicManager : MonoBehaviour
{
    public static TutorialMusicManager Instance;

    public AK.Wwise.Event playTutorialMusic;

    [Header("Optional - use this if main game music keeps playing on Main Menu")]
    public AK.Wwise.Event stopMainGameMusic;

    public float fadeOutDuration = 2f;

    private uint playingID;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        if (stopMainGameMusic != null)
        {
            stopMainGameMusic.Post(gameObject);
        }

        playingID = playTutorialMusic.Post(gameObject);
    }

    public void FadeOutAndDestroy()
    {
        if (playingID != 0)
        {
            AkUnitySoundEngine.StopPlayingID(
                playingID,
                (int)(fadeOutDuration * 1000),
                AkCurveInterpolation.AkCurveInterpolation_Linear
            );
        }

        Destroy(gameObject, fadeOutDuration + 0.2f);
    }

    void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }
}