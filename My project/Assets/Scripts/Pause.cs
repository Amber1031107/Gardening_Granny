using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public GameObject storePrefab;
    public GameObject PauseMenu;

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
            }
        }

    }
    public void MenuSCreen()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame ()
    {
        Application.Quit();
    }
}
