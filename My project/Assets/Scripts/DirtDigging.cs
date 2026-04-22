<<<<<<< HEAD
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AK.Wwise; //Audio
=======
﻿using UnityEngine;
using AK.Wwise;
>>>>>>> MaybeBetterFixForPolishingPurposes

public class DirtDigging : MonoBehaviour, IInteractable
{
    private Renderer rend;
<<<<<<< HEAD
    private FootstepSurfaceTag footstepTag; //Footstep Audio
=======
    private FootstepSurfaceTag footstepTag;
>>>>>>> MaybeBetterFixForPolishingPurposes

    public Material Dirt;
    public Material Grass;

<<<<<<< HEAD
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

    [Header("Audio")] //Audio
    public AK.Wwise.Event digEvent;
    public AK.Wwise.Event plantEvent;
    public AK.Wwise.Event trapEvent;

    private Dictionary<itemType, GameObject> placeablePrefabs;
=======
    [Header("Audio")]
    public AK.Wwise.Event digEvent;
    public AK.Wwise.Event plantEvent;
    public AK.Wwise.Event trapEvent;
>>>>>>> MaybeBetterFixForPolishingPurposes

    public bool CheckDirt;
    public bool PlantIsPlanted;
    public bool TrapIsPlaced;

    public PlayersInventory playerInventory;

    void Start()
    {
        if (playerInventory == null)
            playerInventory = FindAnyObjectByType<PlayersInventory>();

        rend = GetComponent<Renderer>();
<<<<<<< HEAD
        footstepTag = GetComponent<FootstepSurfaceTag>(); //Footstep Audio

        rend.material = Grass;
        CheckDirt = false;
        PlantIsPlanted = false;
        TrapIsPlaced = false;

        if (footstepTag != null)
            footstepTag.surfaceType = FootstepSurface.Grass; //Footstep Audio

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
=======
        footstepTag = GetComponent<FootstepSurfaceTag>();

        rend.material = Grass;

        if (footstepTag != null)
            footstepTag.surfaceType = FootstepSurface.Grass;
>>>>>>> MaybeBetterFixForPolishingPurposes
    }

    public void InteractLeftClick()
    {
        ItemData data = playerInventory.GetSelectedItemData();


<<<<<<< HEAD
        if (holdingPlant)
        {
            if (CheckDirt && !PlantIsPlanted)
            {
                // Check the player actually has stock left
                if (playerInventory.GetSelectedItemCount() == 0) return;

                Instantiate(placeablePrefabs[selected.Value], transform.position, Quaternion.identity);

                plantEvent?.Post(gameObject);//Audio

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

                trapEvent?.Post(gameObject); //Audio

                TrapIsPlaced = true;

                playerInventory.ConsumeItem(itemType.Trap);
            }
            return;
        }

        if (holdingShovel)
        {
            if (!CheckDirt && !TrapIsPlaced)
            {
                digEvent?.Post(gameObject); //Audio

                rend.material = Dirt;
                CheckDirt = true;

                // Change footstep surface to Dirt
                if (footstepTag != null)
                    footstepTag.surfaceType = FootstepSurface.Dirt;

                // Shovel is infinite — no ConsumeItem call needed
=======
        if (data.isShovel)
        {
            if (!CheckDirt && !TrapIsPlaced)
            {
                digEvent?.Post(gameObject);
                rend.material = Dirt;
                CheckDirt = true;
                if (footstepTag != null)
                    footstepTag.surfaceType = FootstepSurface.Dirt;
>>>>>>> MaybeBetterFixForPolishingPurposes
            }
            return;
        }
        // ── Plants on dug soil ────────────────────────────────────────────────
        if (data.plantsOnDirt)
        {
            if (!CheckDirt || PlantIsPlanted) return;
            if (playerInventory.GetSelectedItemCount() == 0) return;
            if (data.placeablePrefab == null)
            {
                Debug.LogWarning($"[DirtDigging] {data.itemID} has no placeablePrefab.");
                return;
            }

            Instantiate(data.placeablePrefab, transform.position, Quaternion.identity);
            plantEvent?.Post(gameObject);
            PlantIsPlanted = true;
            playerInventory.ConsumeItem(data.itemID);
            return;
        }

        // ── Items on unbroken ground ──────────────────────────────────────────
        if (data.plantsOnGrass)
        {
            if (CheckDirt || TrapIsPlaced) return;
            if (playerInventory.GetSelectedItemCount() == 0) return;
            if (data.placeablePrefab == null)
            {
                Debug.LogWarning($"[DirtDigging] {data.itemID} has no placeablePrefab.");
                return;
            }

            Instantiate(data.placeablePrefab, transform.position, Quaternion.identity);
            trapEvent?.Post(gameObject);
            TrapIsPlaced = true;
            playerInventory.ConsumeItem(data.itemID);
            return;
        }
    }

    public void InteractRightClick()
    {
        ItemData data = playerInventory.GetSelectedItemData();
        if (data == null) return;

        // ── Shovel — revert dirt back to grass ───────────────────────────────
        if (data.isShovel && CheckDirt && !PlantIsPlanted)
        {
<<<<<<< HEAD
            if (CheckDirt && !PlantIsPlanted)
            {
                rend.material = Grass;
                CheckDirt = false;

                // Change footstep surface back to Grass
                if (footstepTag != null)
                    footstepTag.surfaceType = FootstepSurface.Grass;
            }
=======
            rend.material = Grass;
            CheckDirt = false;

            if (footstepTag != null)
                footstepTag.surfaceType = FootstepSurface.Grass;
>>>>>>> MaybeBetterFixForPolishingPurposes
        }
    }
}