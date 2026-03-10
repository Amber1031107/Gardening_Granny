using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirtDigging : MonoBehaviour, IInteractable
{
    private Renderer rend;
    public Material Dirt;
    public Material Grass;

    [Header("Placeable Prefabs")]
    public GameObject FlowerSpringPrefab_plant1;
    public GameObject FlowerSpringPrefab_plant2;
    public GameObject FlowerSpringPrefab_plant3;
    public GameObject FlowerSpringPrefab_plant4;
    public GameObject FlowerSpringPrefab_plant5;
    public GameObject FlowerSpringPrefab_plant6;
    public GameObject FlowerSummerPrefab;
    public GameObject FlowerAutumnPrefab;
    public GameObject FlowerWinterPrefab;
    public GameObject TrapPrefab;

    private Dictionary<itemType, GameObject> placeablePrefabs;

    public bool CheckDirt;
    public bool PlantIsPlanted;
    public bool TrapIsPlaced;

    public PlayersInventory playerInventory;

    void Start()
    {
        if (playerInventory == null)
            playerInventory = FindAnyObjectByType<PlayersInventory>();

        rend = GetComponent<Renderer>();
        rend.material = Grass;
        CheckDirt = false;
        PlantIsPlanted = false;
        TrapIsPlaced = false;

        placeablePrefabs = new Dictionary<itemType, GameObject>()
        {
            { itemType.FlowerSpringPlant1, FlowerSpringPrefab_plant1 },
            { itemType.FlowerSpringPlant2, FlowerSpringPrefab_plant2 },
            { itemType.FlowerSpringPlant3, FlowerSpringPrefab_plant3 },
            { itemType.FlowerSpringPlant4, FlowerSpringPrefab_plant4 },
            { itemType.FlowerSpringPlant5, FlowerSpringPrefab_plant5 },
            { itemType.FlowerSpringPlant6, FlowerSpringPrefab_plant6 },
            { itemType.FlowerSummer,       FlowerSummerPrefab },
            { itemType.FlowerAutumn,       FlowerAutumnPrefab },
            { itemType.FlowerWinter,       FlowerWinterPrefab },
            { itemType.Trap,               TrapPrefab }
        };
    }

    public void InteractLeftClick()
    {
        var selected = playerInventory.GetSelectedItemType();
        if (selected == null) return;

        bool holdingPlant = selected.ToString().Contains("Flower");
        bool holdingShovel = selected == itemType.Shovel;
        bool holdingTrap = selected == itemType.Trap;

        if (holdingPlant)
        {
            if (CheckDirt && !PlantIsPlanted)
            {
                // Check the player actually has stock left
                if (playerInventory.GetSelectedItemCount() == 0) return;

                Instantiate(placeablePrefabs[selected.Value], transform.position, Quaternion.identity);
                PlantIsPlanted = true;

                // Use one from the stack
                playerInventory.ConsumeItem(selected.Value);
            }
            return;
        }

        if (holdingTrap)
        {
            if (!CheckDirt && !TrapIsPlaced)
            {
                if (playerInventory.GetSelectedItemCount() == 0) return;

                Instantiate(placeablePrefabs[itemType.Trap], transform.position, Quaternion.identity);
                TrapIsPlaced = true;

                playerInventory.ConsumeItem(itemType.Trap);
            }
            return;
        }

        if (holdingShovel)
        {
            if (!CheckDirt && !TrapIsPlaced)
            {
                rend.material = Dirt;
                CheckDirt = true;
                // Shovel is infinite — no ConsumeItem call needed
            }
        }
    }

    public void InteractRightClick()
    {
        var selected = playerInventory.GetSelectedItemType();
        if (selected == null) return;

        bool holdingShovel = selected == itemType.Shovel;

        if (holdingShovel)
        {
            if (CheckDirt && !PlantIsPlanted)
            {
                rend.material = Grass;
                CheckDirt = false;
            }
        }
    }
}
