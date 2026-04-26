using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public GameObject storePrefab;
    public GameObject PauseMenu;
    public GameObject controlsMenu;

    public AK.Wwise.Event pauseOnSound;
    public AK.Wwise.Event pauseOffSound;
    public AK.Wwise.Event uiClickSound;
    public AK.Wwise.Event pauseAmbience;
    public AK.Wwise.Event resumeAmbience;

    void Start()
    {
        PauseMenu.SetActive(false);
        controlsMenu.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (storePrefab.activeSelf)
            {
                // First escape — close the store only, don't open pause
                storePrefab.SetActive(false);
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
            else
            {
                // Store is closed — toggle pause menu normally
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
                }
            }

            controlsMenu.SetActive(false);
        }
    }

    public void MenuSCreen()
    {
        uiClickSound.Post(gameObject);
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        uiClickSound.Post(gameObject);
        Application.Quit();
    }

    public void Controls()
    {
        controlsMenu.SetActive(true);
        uiClickSound.Post(gameObject);
    }
}
