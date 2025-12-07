using UnityEngine;
using System.Collections;

public class Points : MonoBehaviour, IInteractable
{
  
    public GameObject Kid; 
    public Vector3 spawnPosition;
    public GameObject InvisableWall;
    public Vector3 WallSpawnPosition;

    public void InteractLeftClick()
    {
        Instantiate(Kid, spawnPosition, Quaternion.identity);
        Instantiate(InvisableWall, WallSpawnPosition, Quaternion.identity);


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
