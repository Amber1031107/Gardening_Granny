using UnityEngine;
using AK.Wwise;

public class UIAudioController : MonoBehaviour
{
    [Header("UI Audio")]
    public AK.Wwise.Event uiClickEvent;

    public void PlayUIClick()
    {
        uiClickEvent?.Post(gameObject);
    }
}