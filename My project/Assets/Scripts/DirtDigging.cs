using UnityEngine;
using AK.Wwise;

public class DirtDigging : MonoBehaviour, IInteractable
{
    private Renderer rend;
    private FootstepSurfaceTag footstepTag;

    public Material Dirt;
    public Material Grass;

    [Header("Audio")]
    public AK.Wwise.Event digEvent;
    public AK.Wwise.Event plantEvent;
    public AK.Wwise.Event trapEvent;

    public bool CheckDirt;
    public bool PlantIsPlanted;
    public bool TrapIsPlaced;

    public PlayersInventory playerInventory;

    void Start()
    {
        if (playerInventory == null)
            playerInventory = FindAnyObjectByType<PlayersInventory>();

        rend = GetComponent<Renderer>();
        footstepTag = GetComponent<FootstepSurfaceTag>();

        rend.material = Grass;

        if (footstepTag != null)
            footstepTag.surfaceType = FootstepSurface.Grass;
    }

    public void InteractLeftClick()
    {
        ItemData data = playerInventory.GetSelectedItemData();

        if (data.isShovel)
        {
            if (!CheckDirt && !TrapIsPlaced)
            {
                digEvent?.Post(gameObject);
                rend.material = Dirt;
                CheckDirt = true;
                if (footstepTag != null)
                    footstepTag.surfaceType = FootstepSurface.Dirt;
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
            data.plantSound?.Post(gameObject);   // ← was plantEvent
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
            data.plantSound?.Post(gameObject);   // ← was trapEvent
            TrapIsPlaced = true;
            playerInventory.ConsumeItem(data.itemID);
            return;
        }
    }

    public void InteractRightClick()
    {
        ItemData data = playerInventory.GetSelectedItemData();
        if (data == null) return;

        // Don't revert dirt if a plant is sitting on it — let the plant handle right-click
        if (PlantIsPlanted) return;

        if (data.isShovel && CheckDirt && !PlantIsPlanted)
        {
            rend.material = Grass;
            CheckDirt = false;

            if (footstepTag != null)
                footstepTag.surfaceType = FootstepSurface.Grass;
        }
    }
}