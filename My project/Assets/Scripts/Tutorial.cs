using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour
{

    public AK.Wwise.Event uiClickSound; //Audio

    //public float delayTime = 5f;  // Time in seconds before changing the scene
    // public string sceneName = "Tutorial";  // Name of the scene to load
    public void Next()
   {
<<<<<<< HEAD
        uiClickSound.Post(gameObject); //Audio
        SceneManager.LoadScene("PhilBuild");
=======
        SceneManager.LoadScene("SpringSeason1");
>>>>>>> MaybeBetterFixForPolishingPurposes
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
