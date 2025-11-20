using UnityEngine;
using System.Collections;

public class DirtDigging : MonoBehaviour, IInteractable
{
    private Renderer rend;
    public Material Dirt;
    public Material Grass;
    public GameObject Plant;
    public bool CheckDirt;
    public bool PlantIsPlanted;
    void Start()
    {
        rend = GetComponent<Renderer>();
        rend.material = Grass;
        CheckDirt = false;
        PlantIsPlanted = false;
    }

    public void InteractLeftClick()
    {
        Debug.Log("tileInteracted");
        
        if(CheckDirt == true)
        {
            Debug.Log("PlantCanBePlanted");
            Instantiate(Plant, transform.position, Quaternion.identity);
            PlantIsPlanted = true;
        }
        else
        {
            rend.material = Dirt;
            CheckDirt = true;
        }
    }
    public void InteractRightClick()
    {
        if (PlantIsPlanted == false)
        {
            Debug.Log("tileInteracted");
            rend.material = Grass;
            CheckDirt = false;
        }
        
    }
}
