using Unity.VisualScripting;
using UnityEngine;
using System.Collections;

public class Planting : MonoBehaviour, IInteractable
{
    public GameObject[] DirtGround;
    public GameObject Plant;
    public MeshRenderer[] DirtMaterialRenderer;
    public Material DirtMaterialCheck;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        MeshRenderer DirtMaterialRenderer = DirtGround.GetComponent<MeshRenderer>();
        Material DirtMaterialCheck = DirtMaterialRenderer.material;
    }

    public void InteractLeftClick()
    {
        if(DirtMaterialCheck.name == "Dirt")
        {
            Debug.Log("plantPlanted");
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
