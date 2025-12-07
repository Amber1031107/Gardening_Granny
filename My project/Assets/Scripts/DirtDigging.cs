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

    public PlayersInventory playerInventory;
    void Start()
    {
        if (playerInventory == null)
        {
            playerInventory = GetComponent<PlayersInventory>();
        }
        rend = GetComponent<Renderer>();
        rend.material = Grass;
        CheckDirt = false;
        PlantIsPlanted = false;
        TrapIsPlaced = false;
    }

    public void InteractLeftClick()
    {
        Debug.Log("tileInteracted");
        
        if(CheckDirt == true && PlantIsPlanted == false)
        {
            Instantiate(Plant, transform.position, Quaternion.identity); // For example, plant flowers
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
            else if (CheckDirt == false && TrapIsPlaced == false)
            {
                 Instantiate(Traps, transform.position, Quaternion.identity);
                 TrapIsPlaced = true;

            }
        }
        
    }
}
