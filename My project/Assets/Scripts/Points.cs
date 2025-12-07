using UnityEngine;
using System.Collections;

public class Points : MonoBehaviour, IInteractable
{
    public int PlayerPointsTotal;

    public void InteractLeftClick()
    {
        GameObject[] allPlants = GameObject.FindGameObjectsWithTag("Flower");
        Debug.Log("Number of plants in the scene: " + allPlants.Length);

        // Count all objects with the tag "Trap"
        GameObject[] allTraps = GameObject.FindGameObjectsWithTag("Trap");
        Debug.Log("Number of traps in the scene: " + allTraps.Length);

        PlayerPointsTotal = allPlants.Length - allTraps.Length;
        Debug.Log("TotalPoints: " + PlayerPointsTotal);
    }

    public void InteractRightClick()
    {
        throw new System.NotImplementedException();
    }

    void Start()
    {
        // Count all objects with the tag "Plant"
        
    }
}
