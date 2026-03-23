using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public GameObject storePrefab;
    public GameObject PauseMenu;

    public AK.Wwise.Event pauseOnSound; //Audio
    public AK.Wwise.Event pauseOffSound;
    public AK.Wwise.Event uiClickSound;
    public AK.Wwise.Event pauseAmbience;
    public AK.Wwise.Event resumeAmbience;

    void Start()
    {
        PauseMenu.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (storePrefab.activeSelf)
            {
                // First escape — close the store, don't open pause menu
                storePrefab.SetActive(false);
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
            else
            {
                // Store is closed — toggle pause menu
                bool isPaused = PauseMenu.activeSelf;
                PauseMenu.SetActive(!isPaused);
                Cursor.visible = !isPaused;
                Cursor.lockState = isPaused ? CursorLockMode.Locked : CursorLockMode.None;
                Time.timeScale = isPaused ? 1f : 0f; // optional: freeze game when paused

                if (!isPaused)
                {
                    pauseOnSound.Post(gameObject);   // Audio
                    pauseAmbience.Post(gameObject);
                }
                else
                {
                    pauseOffSound.Post(gameObject);
                    resumeAmbience.Post(gameObject); // Audio
                }
            }
        }

    }
    public void MenuSCreen()
    {
        uiClickSound.Post(gameObject); //Audio
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame ()
    {
        uiClickSound.Post(gameObject); //Audio
        Application.Quit();
    }
}
