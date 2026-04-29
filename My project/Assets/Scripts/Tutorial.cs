using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;



public class Tutorial : MonoBehaviour
{
    //public float delayTime = 5f;  // Time in seconds before changing the scene
    // public string sceneName = "Tutorial";  // Name of the scene to load

    public AK.Wwise.Event uiClickSound; //Audio

    public void Next()

   {
        uiClickSound?.Post(gameObject); 

        if (TutorialMusicManager.Instance != null)
        {
            TutorialMusicManager.Instance.FadeOutAndDestroy();
        }

        SceneManager.LoadScene("SpringSeason1");
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
