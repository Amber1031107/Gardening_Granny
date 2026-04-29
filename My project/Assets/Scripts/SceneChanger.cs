using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
        public void LoadScene(string MainMenu)
    {
                SceneManager.LoadScene(MainMenu);
    }
}
