using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public GameObject storePrefab;
    public GameObject PauseMenu;
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
    public GameObject ControlsMenu;
=======
    public GameObject howToPlayPanel;
>>>>>>> e7eb6f0 (Updates after playtesting)
=======
    public GameObject howToPlayPanel;
>>>>>>> e7eb6f0 (Updates after playtesting)
=======
=======
>>>>>>> f4f8a07 (no message)
    public GameObject howToPlayPanel;
=======
    public GameObject ControlsMenu;
>>>>>>> 0f02715 (Fixing up after rebase)
<<<<<<< HEAD
>>>>>>> b46e5f7 (no message)
=======
=======
    public GameObject ControlsMenu;
=======
    public GameObject howToPlayPanel;
>>>>>>> e7eb6f0 (Updates after playtesting)
>>>>>>> 3a79e98 (no message)
>>>>>>> f4f8a07 (no message)

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
=======
=======
>>>>>>> f4f8a07 (no message)

        if (howToPlayPanel != null)
            howToPlayPanel.SetActive(false);
=======
        ControlsMenu.SetActive(false);
>>>>>>> 0f02715 (Fixing up after rebase)
<<<<<<< HEAD
>>>>>>> b46e5f7 (no message)
=======
=======
        ControlsMenu.SetActive(false);
=======

        if (howToPlayPanel != null)
            howToPlayPanel.SetActive(false);
>>>>>>> e7eb6f0 (Updates after playtesting)
>>>>>>> 3a79e98 (no message)
>>>>>>> f4f8a07 (no message)
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
<<<<<<< HEAD
=======
>>>>>>> f4f8a07 (no message)
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
<<<<<<< HEAD
>>>>>>> e7eb6f0 (Updates after playtesting)
=======
    public void OpenHowToPlay()
    {
        PauseMenu.SetActive(false);
        howToPlayPanel.SetActive(true);
>>>>>>> 620b465 (Got How to Play Working)
=======
>>>>>>> 3a79e98 (no message)
>>>>>>> f4f8a07 (no message)
    }

    public void BackButton()
    {
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
=======
>>>>>>> f4f8a07 (no message)
        howToPlayPanel.SetActive(false);
        PauseMenu.SetActive(true);
=======
        uiClickSound.Post(gameObject);

        if (howToPlayPanel != null)
            howToPlayPanel.SetActive(false);
<<<<<<< HEAD
>>>>>>> e7eb6f0 (Updates after playtesting)
=======
        howToPlayPanel.SetActive(false);
        PauseMenu.SetActive(true);
>>>>>>> 620b465 (Got How to Play Working)
=======
>>>>>>> 3a79e98 (no message)
>>>>>>> f4f8a07 (no message)
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
<<<<<<< HEAD
<<<<<<< HEAD
=======
}
=======
>>>>>>> b46e5f7 (no message)
=======
}
=======
=======
>>>>>>> 3a79e98 (no message)
>>>>>>> f4f8a07 (no message)

    public void ControlScreen()
    {
        ControlsMenu.SetActive(true);
    }
}
<<<<<<< HEAD
<<<<<<< HEAD
=======
}
>>>>>>> e7eb6f0 (Updates after playtesting)
=======
}
>>>>>>> e7eb6f0 (Updates after playtesting)
=======
>>>>>>> 0f02715 (Fixing up after rebase)
>>>>>>> b46e5f7 (no message)
=======
>>>>>>> 0f02715 (Fixing up after rebase)
=======
=======
}
>>>>>>> e7eb6f0 (Updates after playtesting)
>>>>>>> 3a79e98 (no message)
>>>>>>> f4f8a07 (no message)
