using UnityEngine;
using System.Collections;

public class DirtDigging : MonoBehaviour, IInteractable
{
    private Renderer rend;
    public Material Dirt;
    public Material Grass;
    public GameObject Plant;
    public GameObject Traps;
    public bool CheckDirt;
    public bool PlantIsPlanted;
    public bool TrapIsPlaced;
    void Start()
    {
        rend = GetComponent<Renderer>();
        rend.material = Grass;
        CheckDirt = false;
        PlantIsPlanted = false;
        TrapIsPlaced = false;
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
        else if (TrapIsPlaced == false)
        {
            rend.material = Dirt;
            CheckDirt = true;
        }
    }
    public void InteractRightClick()
    {
        if (PlantIsPlanted == false)
        {
            if (CheckDirt == true)
            {
                Debug.Log("tileInteracted");
                rend.material = Grass;
                CheckDirt = false;
            }
            else if (CheckDirt == false)
            {
                Instantiate(Traps, transform.position, Quaternion.identity);
                TrapIsPlaced = true;
            }
            
        }
        
    }
}
