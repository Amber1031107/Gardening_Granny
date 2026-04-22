using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour
{
    //public float delayTime = 5f;  // Time in seconds before changing the scene
   // public string sceneName = "Tutorial";  // Name of the scene to load
   public void Next()
   {
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
        SceneManager.LoadScene("SpringSeason1");
=======
        SceneManager.LoadScene("Build");
>>>>>>> b4c89e6 (no message)
=======
        uiClickSound.Post(gameObject); //Audio
        SceneManager.LoadScene("PhilBuild");
>>>>>>> e7eb6f0 (Updates after playtesting)
=======
=======
>>>>>>> 33591fd (no message)
        uiClickSound.Post(gameObject); //Audio
        SceneManager.LoadScene("PhilBuild");
=======
        SceneManager.LoadScene("Build");
>>>>>>> 5fee5fe (pulling audio into programming)
<<<<<<< HEAD
>>>>>>> 47b9640 (no message)
=======
=======
        SceneManager.LoadScene("SpringSeason1");
>>>>>>> cd50569 (no message)
>>>>>>> 33591fd (no message)
   }

    //void Start()
   // {
   //     // Start the coroutine to change the scene after a delay
   //     StartCoroutine(ChangeSceneAfterDelay());
   // }

    // Coroutine to change the scene after a delay
 //   private IEnumerator ChangeSceneAfterDelay()
  //  {
        // Wait for the specified delay time
   //     yield return new WaitForSeconds(delayTime);

        // Load the new scene
      //  SceneManager.LoadScene(sceneName);
   // }

     
}
