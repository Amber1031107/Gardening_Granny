using Unity.VisualScripting;
using UnityEngine;
using System.Collections;

public class Planting : MonoBehaviour, IInteractable
{
     public GameObject Plant;

    public Renderer rend;
    public Material DirtMaterial;

    void Start()
    {
        if (rend == null) rend = GetComponent<Renderer>();
    }

    public void InteractLeftClick()
    {
        if (rend.material == DirtMaterial)
        {
            Debug.Log("plantPlanted");
            Instantiate(Plant, transform.position, Quaternion.identity);
        }
        else
        {
            Debug.Log("PlantNotPlanted");
        }
    }

    public void InteractRightClick()
    {
        throw new System.NotImplementedException();
    }
}
