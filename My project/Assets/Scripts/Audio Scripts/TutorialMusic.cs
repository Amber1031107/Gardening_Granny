using UnityEngine;

public class TutorialMusicManager : MonoBehaviour
{
    public AK.Wwise.Event playTutorialMusic;

    public float fadeOutDuration = 2f;

    private static TutorialMusicManager instance;
    private uint playingID;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
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
}