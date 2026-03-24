using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public GameObject storePrefab;
    public GameObject PauseMenu;
    public GameObject howToPlayPanel;

    public AK.Wwise.Event pauseOnSound;
    public AK.Wwise.Event pauseOffSound;
    public AK.Wwise.Event uiClickSound;
    public AK.Wwise.Event pauseAmbience;
    public AK.Wwise.Event resumeAmbience;

    void Start()
    {
        PauseMenu.SetActive(false);

        if (howToPlayPanel != null)
            howToPlayPanel.SetActive(false);
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
        uiClickSound.Post(gameObject);
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        uiClickSound.Post(gameObject);
        Application.Quit();
    }
}