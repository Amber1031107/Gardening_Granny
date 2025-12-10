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

    void Start()
    {
        Fances.SetActive(true);
    }
    public void InteractLeftClick()
    {
        Instantiate(Kid, spawnPosition, Quaternion.identity);
        Instantiate(InvisableWall, WallSpawnPosition, Quaternion.identity);
        Fances.SetActive(false);

    }

    public void InteractRightClick()
    {
        throw new System.NotImplementedException();
    }


}
