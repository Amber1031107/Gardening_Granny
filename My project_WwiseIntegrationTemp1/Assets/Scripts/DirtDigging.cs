using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirtDigging : MonoBehaviour, IInteractable
{
    private Renderer rend;

    public Material Dirt;
    public Material Grass;

    [Header("Placeable Prefabs")]
    public GameObject FlowerSpringPrefab;
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

        // Create mapping
        placeablePrefabs = new Dictionary<itemType, GameObject>()
        {
            { itemType.FlowerSpring, FlowerSpringPrefab },
            { itemType.FlowerSummer, FlowerSummerPrefab },
            { itemType.FlowerAutumn, FlowerAutumnPrefab },
            { itemType.FlowerWinter, FlowerWinterPrefab },
            { itemType.Trap, TrapPrefab }
        };
    }

    private itemType? GetSelectedItemType()
    {
        int index = playerInventory.selectItem - 1;

        if (index < 0 || index >= playerInventory.inventoryList.Count)
            return null;

        return playerInventory.inventoryList[index];
    }


    public void InteractLeftClick()
    {
        var selected = GetSelectedItemType();
        if (selected == null) return;

        bool holdingPlant = selected.ToString().Contains("Flower");
        bool holdingShovel = selected == itemType.Shovel;


        if (holdingPlant)
        {
            if (CheckDirt && !PlantIsPlanted)
            {
                GameObject prefab = placeablePrefabs[selected.Value];
                Instantiate(prefab, transform.position, Quaternion.identity);
                PlantIsPlanted = true;
            }
            return;
        }


        if (holdingShovel)
        {
            if (!CheckDirt && !TrapIsPlaced)
            {
                rend.material = Dirt;
                CheckDirt = true;
            }
        }
    }


    public void InteractRightClick()
    {
        var selected = GetSelectedItemType();
        if (selected == null) return;

        bool holdingTrap = selected == itemType.Trap;
        bool holdingShovel = selected == itemType.Shovel;


        if (holdingTrap)
        {
            if (!CheckDirt && !TrapIsPlaced)
            {
                GameObject prefab = placeablePrefabs[itemType.Trap];
                Instantiate(prefab, transform.position, Quaternion.identity);
                TrapIsPlaced = true;
            }
            return;
        }

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
