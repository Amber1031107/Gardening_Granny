using UnityEngine;

public class FootstepSwitchTest : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            AkSoundEngine.SetSwitch("Surfaces", "Grass", gameObject);
            AkSoundEngine.PostEvent("Play_Footsteps", gameObject);
            Debug.Log("Played Grass footstep");
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            AkSoundEngine.SetSwitch("Surfaces", "Dirt", gameObject);
            AkSoundEngine.PostEvent("Play_Footsteps", gameObject);
            Debug.Log("Played Dirt footstep");
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            AkSoundEngine.SetSwitch("Surfaces", "Concrete", gameObject);
            AkSoundEngine.PostEvent("Play_Footsteps", gameObject);
            Debug.Log("Played Concrete footstep");
        }
    }
}