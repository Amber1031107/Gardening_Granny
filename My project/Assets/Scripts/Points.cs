using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

public class Points : MonoBehaviour, IInteractable
{
  
    public GameObject Kid; 
    public Vector3 spawnPosition;
    public GameObject InvisableWall;
    public GameObject Fances;
    public Vector3 WallSpawnPosition;

    public int startGame;

    void Start()
    {
        Fances.SetActive(true);
    }
    public void InteractLeftClick()
    {
        GameObject[] allPlants = GameObject.FindGameObjectsWithTag("Flower");
        Debug.Log("Number of plants in the scene: " + allPlants.Length);

            // Count all objects with the tag "Trap"
        GameObject[] allTraps = GameObject.FindGameObjectsWithTag("Trap");
        Debug.Log("Number of traps in the scene: " + allTraps.Length);
        startGame = allPlants.Length + allTraps.Length;

        if (startGame > 1)
        {
            Instantiate(Kid, spawnPosition, Quaternion.identity);
            Instantiate(InvisableWall, WallSpawnPosition, Quaternion.identity);
            Fances.SetActive(false);

            // Switch ambience to night
            FindObjectOfType<SeasonAmbienceController>().SetNight();
        }

        

    }

    public void InteractRightClick()
    {
        throw new System.NotImplementedException();
    }


}
