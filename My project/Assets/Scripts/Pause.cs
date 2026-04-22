using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public GameObject storePrefab;
    public GameObject PauseMenu;
<<<<<<< HEAD
    public GameObject howToPlayPanel;

    public AK.Wwise.Event pauseOnSound;
=======
    public GameObject ControlsMenu;

    public AK.Wwise.Event pauseOnSound; //Audio
>>>>>>> MaybeBetterFixForPolishingPurposes
    public AK.Wwise.Event pauseOffSound;
    public AK.Wwise.Event uiClickSound;
    public AK.Wwise.Event pauseAmbience;
    public AK.Wwise.Event resumeAmbience;

    void Start()
    {
        PauseMenu.SetActive(false);
<<<<<<< HEAD

        if (howToPlayPanel != null)
            howToPlayPanel.SetActive(false);
=======
        ControlsMenu.SetActive(false);
>>>>>>> MaybeBetterFixForPolishingPurposes
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (storePrefab.activeSelf)
            {
                storePrefab.SetActive(false);
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
            else
            {
                bool isPaused = PauseMenu.activeSelf;
                PauseMenu.SetActive(!isPaused);
                Cursor.visible = !isPaused;
                Cursor.lockState = isPaused ? CursorLockMode.Locked : CursorLockMode.None;
<<<<<<< HEAD
                Time.timeScale = isPaused ? 1f : 0f;

                if (!isPaused)
                {
                    pauseOnSound.Post(gameObject);
=======
                Time.timeScale = isPaused ? 1f : 0f; // optional: freeze game when paused

                if (!isPaused)
                {
                    pauseOnSound.Post(gameObject);   // Audio
>>>>>>> MaybeBetterFixForPolishingPurposes
                    pauseAmbience.Post(gameObject);
                }
                else
                {
                    pauseOffSound.Post(gameObject);
<<<<<<< HEAD
                    resumeAmbience.Post(gameObject);

                    if (howToPlayPanel != null)
                        howToPlayPanel.SetActive(false);
=======
                    resumeAmbience.Post(gameObject); // Audio
>>>>>>> MaybeBetterFixForPolishingPurposes
                }
            }
        }
    }

    public void OpenHowToPlay()
    {
        PauseMenu.SetActive(false);
        howToPlayPanel.SetActive(true);
    }

    public void BackButton()
    {
        howToPlayPanel.SetActive(false);
        PauseMenu.SetActive(true);
    }

    public void MenuSCreen()
    {
<<<<<<< HEAD
        uiClickSound.Post(gameObject);
        Time.timeScale = 1f;
=======
        uiClickSound.Post(gameObject); //Audio
>>>>>>> MaybeBetterFixForPolishingPurposes
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
<<<<<<< HEAD
        uiClickSound.Post(gameObject);
        Application.Quit();
    }
}
=======
        uiClickSound.Post(gameObject); //Audio
        Application.Quit();
    }

    public void ControlScreen()
    {
        ControlsMenu.SetActive(true);
    }
}
>>>>>>> MaybeBetterFixForPolishingPurposes
