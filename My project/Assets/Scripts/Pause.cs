using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public GameObject storePrefab;
    public GameObject PauseMenu;
<<<<<<< HEAD
<<<<<<< HEAD
    public GameObject ControlsMenu;
=======
    public GameObject howToPlayPanel;
>>>>>>> e7eb6f0 (Updates after playtesting)
=======
    public GameObject howToPlayPanel;
>>>>>>> e7eb6f0 (Updates after playtesting)

    public AK.Wwise.Event pauseOnSound;
    public AK.Wwise.Event pauseOffSound;
    public AK.Wwise.Event uiClickSound;
    public AK.Wwise.Event pauseAmbience;
    public AK.Wwise.Event resumeAmbience;

    void Start()
    {
        PauseMenu.SetActive(false);
<<<<<<< HEAD
<<<<<<< HEAD
        ControlsMenu.SetActive(false);
=======

        if (howToPlayPanel != null)
            howToPlayPanel.SetActive(false);
>>>>>>> e7eb6f0 (Updates after playtesting)
=======

        if (howToPlayPanel != null)
            howToPlayPanel.SetActive(false);
>>>>>>> e7eb6f0 (Updates after playtesting)
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
                Time.timeScale = isPaused ? 1f : 0f;

                if (!isPaused)
                {
                    pauseOnSound.Post(gameObject);
                    pauseAmbience.Post(gameObject);
                }
                else
                {
                    pauseOffSound.Post(gameObject);
                    resumeAmbience.Post(gameObject);

                    if (howToPlayPanel != null)
                        howToPlayPanel.SetActive(false);
                }
            }
        }
    }

<<<<<<< HEAD
<<<<<<< HEAD
    public void OpenHowToPlay()
    {
        PauseMenu.SetActive(false);
        howToPlayPanel.SetActive(true);
=======
    public void HowToPlayButton()
    {
        uiClickSound.Post(gameObject);

        if (howToPlayPanel != null)
            howToPlayPanel.SetActive(true);
>>>>>>> e7eb6f0 (Updates after playtesting)
=======
    public void OpenHowToPlay()
    {
        PauseMenu.SetActive(false);
        howToPlayPanel.SetActive(true);
>>>>>>> 620b465 (Got How to Play Working)
    }

    public void BackButton()
    {
<<<<<<< HEAD
<<<<<<< HEAD
        howToPlayPanel.SetActive(false);
        PauseMenu.SetActive(true);
=======
        uiClickSound.Post(gameObject);

        if (howToPlayPanel != null)
            howToPlayPanel.SetActive(false);
>>>>>>> e7eb6f0 (Updates after playtesting)
=======
        howToPlayPanel.SetActive(false);
        PauseMenu.SetActive(true);
>>>>>>> 620b465 (Got How to Play Working)
    }

    public void MenuSCreen()
    {
        uiClickSound.Post(gameObject);
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        uiClickSound.Post(gameObject);
        Application.Quit();
    }
<<<<<<< HEAD
<<<<<<< HEAD

    public void ControlScreen()
    {
        ControlsMenu.SetActive(true);
    }
}
=======
}
>>>>>>> e7eb6f0 (Updates after playtesting)
=======
}
>>>>>>> e7eb6f0 (Updates after playtesting)
