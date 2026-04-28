using UnityEngine;
using AK.Wwise;

public class PlacedPlant : MonoBehaviour, IInteractable
{
    [Header("Item Identity")]
    [Tooltip("Must match the itemID in ItemData for this plant/trap.")]
    public string itemID;

    [Tooltip("Is this a trap (plantsOnGrass) rather than a plant (plantsOnDirt)?")]
    public bool isTrap = false;

    [Header("Rotation")]
    [Tooltip("Degrees to rotate per right-click.")]
    public float rotationStep = 45f;

    [Header("Audio")]
    public AK.Wwise.Event pickupEvent;
    public AK.Wwise.Event rotateEvent;


    private PlayersInventory playerInventory;
    private DirtDigging parentTile; // The tile this object sits on

    void Start()
    {
        playerInventory = FindAnyObjectByType<PlayersInventory>();

        if (playerInventory == null)
            Debug.LogError("[PlacedPlant] Could not find PlayersInventory in scene.");
        Debug.Log($"[PlacedPlant] IInteractable implemented: {this is IInteractable}");

        // Find the DirtDigging tile underneath this object
        parentTile = FindTileBelow();

        if (parentTile == null)
            Debug.LogWarning($"[PlacedPlant] '{gameObject.name}' could not locate a DirtDigging tile below it. " +
                             "Pickup will still work but tile state won't be restored.");
    }


    public void InteractLeftClick()
    {
        if (playerInventory == null) return;

        // Only allow pickup when the shovel is selected (same design pattern as DirtDigging)
        ItemData selected = playerInventory.GetSelectedItemData();
        if (selected == null || !selected.isShovel)
        {
            Debug.Log("[PlacedPlant] Equip the shovel to pick this up.");
            return;
        }

        // Restore tile state
        if (parentTile != null)
        {
            if (isTrap)
                parentTile.TrapIsPlaced = false;
            else
                parentTile.PlantIsPlanted = false;
        }

        // Return item to inventory
        playerInventory.AddItem(itemID);

        pickupEvent?.Post(gameObject);
        FindObjectOfType<TutorialManager>()?.NotifyItemPickedUp();
        Destroy(gameObject);
    }

    /// <summary>Right-click: rotate the object by <see cref="rotationStep"/> degrees.</summary>
    public void InteractRightClick()
    {
        transform.Rotate(Vector3.up, rotationStep, Space.World);
        rotateEvent?.Post(gameObject);
        Debug.Log($"[PlacedPlant] '{gameObject.name}' rotated to Y={transform.eulerAngles.y:F1}°");
        FindObjectOfType<TutorialManager>()?.NotifyItemRotated();
    }


    private DirtDigging FindTileBelow()
    {
        // Start just above the object's pivot, search 2 units down
        Vector3 origin = transform.position + Vector3.up * 0.1f;
        float searchRadius = 0.6f;
        float searchDistance = 2f;

        RaycastHit[] hits = Physics.SphereCastAll(origin, searchRadius, Vector3.down, searchDistance);
        foreach (RaycastHit hit in hits)
        {
            DirtDigging tile = hit.collider.GetComponent<DirtDigging>();
            if (tile != null)
                return tile;
        }
        return null;
    }
}
