using UnityEngine;

public class LookAtLetter : MonoBehaviour
{
    [Header("Comic and Letter")]
    public GameObject MagnnifyingGlassButton;
    public GameObject LetterUi;
    public GameObject NextButton;
    public GameObject BackButton;

    [Header("Other")]
    public GameObject MaginfyinglassTwo;
    public GameObject BackButtonTwo;
    public GameObject LetterReadable;
    public GameObject ComicScreen;
    public AK.Wwise.Event uiClickSound;

    void Start()
    {
        MagnnifyingGlassButton.SetActive(true);
        NextButton.SetActive(false);
        BackButton.SetActive(false);
        LetterUi.SetActive(false);
        MaginfyinglassTwo.SetActive(false);
        BackButtonTwo.SetActive(false);
        LetterReadable.SetActive(false);
        ComicScreen.SetActive(true);
    }
    public void ReadLetter()
    {
        uiClickSound.Post(gameObject);
        MagnnifyingGlassButton.SetActive(false);
        NextButton.SetActive(true);
        BackButton.SetActive(false);
        LetterUi.SetActive(false);
        MaginfyinglassTwo.SetActive(false);
        BackButtonTwo.SetActive(true);
        LetterReadable.SetActive(true);
        ComicScreen.SetActive(false);
    }

    public void BackAgainTwo()
    {
        uiClickSound.Post(gameObject);
        MagnnifyingGlassButton.SetActive(false);
        NextButton.SetActive(false);
        BackButton.SetActive(true);
        LetterUi.SetActive(true);
        MaginfyinglassTwo.SetActive(true);
        BackButtonTwo.SetActive(false);
        LetterReadable.SetActive(false);
        ComicScreen.SetActive(false);
    }

    public void SeeLetter()
    {
        uiClickSound.Post(gameObject);
        MagnnifyingGlassButton.SetActive(false);
        NextButton.SetActive(false);
        BackButton.SetActive(true);
        LetterUi.SetActive(true);
        MaginfyinglassTwo.SetActive(true);
        BackButtonTwo.SetActive(false);
        LetterReadable.SetActive(false);
        ComicScreen.SetActive(false);

    }


    public void GoBack()
    {
        uiClickSound.Post(gameObject);
        MagnnifyingGlassButton.SetActive(true);
        NextButton.SetActive(false);
        BackButton.SetActive(false);
        LetterUi.SetActive(false);
        MaginfyinglassTwo.SetActive(false);
        BackButtonTwo.SetActive(false);
        LetterReadable.SetActive(false);
        ComicScreen.SetActive(true);
    }
}
