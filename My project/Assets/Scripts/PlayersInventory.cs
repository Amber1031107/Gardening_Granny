using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayersInventory : MonoBehaviour
{
    [Header("General")]
    // Ordered list of unique item types (one slot per item type)
    public List<itemType> inventoryList = new List<itemType>();
    // How many of each item type the player currently owns
    public Dictionary<itemType, int> itemCounts = new Dictionary<itemType, int>();
    public int selectItem;

    [Space(20)]
    [Header("Item gameobjects")]
    [SerializeField] GameObject Flower_Spring_Item_Flower1;
    [SerializeField] GameObject Flower_Spring_Item_Flower2;
    [SerializeField] GameObject Flower_Spring_Item_Flower3;
    [SerializeField] GameObject Flower_Spring_Item_Flower4;
    [SerializeField] GameObject Flower_Spring_Item_Flower5;
    [SerializeField] GameObject Flower_Spring_Item_Flower6;
    [SerializeField] GameObject Trap_Item;
    [SerializeField] GameObject Shovel_Item;

    private Dictionary<itemType, GameObject> itemSetActive = new Dictionary<itemType, GameObject>();

    void Start()
    {
        itemSetActive.Add(itemType.FlowerSpringPlant1, Flower_Spring_Item_Flower1);
        itemSetActive.Add(itemType.FlowerSpringPlant2, Flower_Spring_Item_Flower2);
        itemSetActive.Add(itemType.FlowerSpringPlant3, Flower_Spring_Item_Flower3);
        itemSetActive.Add(itemType.FlowerSpringPlant4, Flower_Spring_Item_Flower4);
        itemSetActive.Add(itemType.FlowerSpringPlant5, Flower_Spring_Item_Flower5);
        itemSetActive.Add(itemType.FlowerSpringPlant6, Flower_Spring_Item_Flower6);
        itemSetActive.Add(itemType.Trap, Trap_Item);
        itemSetActive.Add(itemType.Shovel, Shovel_Item);

        // Shovel always starts in inventory — -1 means infinite uses
        inventoryList.Add(itemType.Shovel);
        itemCounts[itemType.Shovel] = -1;
        selectItem = 1;

        DeactivateAll();
        NewItemSelected();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) SelectSlot(1);
        else if (Input.GetKeyDown(KeyCode.Alpha2)) SelectSlot(2);
        else if (Input.GetKeyDown(KeyCode.Alpha3)) SelectSlot(3);
        else if (Input.GetKeyDown(KeyCode.Alpha4)) SelectSlot(4);
        else if (Input.GetKeyDown(KeyCode.Alpha5)) SelectSlot(5);
        else if (Input.GetKeyDown(KeyCode.Alpha6)) SelectSlot(6);
        else if (Input.GetKeyDown(KeyCode.Alpha7)) SelectSlot(7);
        else if (Input.GetKeyDown(KeyCode.Alpha8)) SelectSlot(8);
    }

   

    public void AddItem(itemType newItem)
    {
        if (itemCounts.ContainsKey(newItem))
        {
            // Item already has a slot — just stack it
            itemCounts[newItem]++;
            Debug.Log($"[Inventory] {newItem} stacked to {itemCounts[newItem]}");
        }
        else
        {
            // Brand new item type — create a new slot
            inventoryList.Add(newItem);
            itemCounts[newItem] = 1;
            Debug.Log($"[Inventory] New slot for {newItem}. Total slots: {inventoryList.Count}");
        }

        // Auto-switch to the bought item
        selectItem = inventoryList.IndexOf(newItem) + 1;
        NewItemSelected();
    }

   

    public void ConsumeItem(itemType usedItem)
    {
        if (!itemCounts.ContainsKey(usedItem)) return;
        if (itemCounts[usedItem] == -1) return; // infinite (Shovel)

        itemCounts[usedItem]--;
        Debug.Log($"[Inventory] {usedItem} remaining: {itemCounts[usedItem]}");

        if (itemCounts[usedItem] <= 0)
        {
            inventoryList.Remove(usedItem);
            itemCounts.Remove(usedItem);
            Debug.Log($"[Inventory] {usedItem} depleted — slot removed.");

            // Keep selection in range
            if (selectItem > inventoryList.Count)
                selectItem = Mathf.Max(1, inventoryList.Count);
        }

        NewItemSelected();
    }

    

    public itemType? GetSelectedItemType()
    {
        int index = selectItem - 1;
        if (index < 0 || index >= inventoryList.Count) return null;
        return inventoryList[index];
    }

    // Returns remaining count of currently selected item (-1 = infinite)
    public int GetSelectedItemCount()
    {
        var selected = GetSelectedItemType();
        if (selected == null) return 0;
        return itemCounts.TryGetValue(selected.Value, out int count) ? count : 0;
    }

    

    private void SelectSlot(int slot)
    {
        selectItem = slot;
        NewItemSelected();
    }

    private void NewItemSelected()
    {
        DeactivateAll();

        if (selectItem >= 1 && selectItem <= inventoryList.Count)
        {
            itemSetActive[inventoryList[selectItem - 1]].SetActive(true);
        }
        else
        {
            Debug.Log($"[Inventory] Slot {selectItem} is empty.");
        }
    }

    private void DeactivateAll()
    {
        foreach (var kvp in itemSetActive)
            kvp.Value.SetActive(false);
    }
}