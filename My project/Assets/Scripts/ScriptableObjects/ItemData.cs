using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Inventory/Item Data")]
public class ItemData : ScriptableObject
{
    public string itemID;
    public string displayName;        // friendly name shown in hotbar
    public GameObject handPrefab;
    public GameObject placeablePrefab;
    public Sprite icon;
    public int cost;
    public bool isInfinite;
    public bool isShovel;
    public bool plantsOnDirt;
    public bool plantsOnGrass;

    [Header("Audio")]
    public AK.Wwise.Event selectionSound;
}
