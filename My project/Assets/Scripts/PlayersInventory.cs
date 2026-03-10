using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class PlayersInventory : MonoBehaviour
{
    [Header("General")]
    public List<itemType> inventoryList = new List<itemType>();
    public int selectItem;

    [Space(20)]
    [Header("Item gameobjects")]
    [SerializeField] GameObject Flower_Spring_Item_Flower1;
    [SerializeField] GameObject Flower_Spring_Item_Flower2;
    [SerializeField] GameObject Flower_Spring_Item_Flower3;
    [SerializeField] GameObject Flower_Spring_Item_Flower4;
    [SerializeField] GameObject Flower_Spring_Item_Flower5;
    [SerializeField] GameObject Flower_Spring_Item_Flower6;
   // [SerializeField] GameObject Flower_Summer_Item;
   // [SerializeField] GameObject Flower_Autumn_Item;
    //[SerializeField] GameObject Flower_Winter_Item;
    [SerializeField] GameObject Trap_Item;
    [SerializeField] GameObject Shovel_Item;

    private Dictionary<itemType, GameObject> itemSetActive = new Dictionary<itemType, GameObject>() { };
    private int selectedItem;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        itemSetActive.Add(itemType.FlowerSpringPlant1, Flower_Spring_Item_Flower1);
        itemSetActive.Add(itemType.FlowerSpringPlant2, Flower_Spring_Item_Flower2);
        itemSetActive.Add(itemType.FlowerSpringPlant3, Flower_Spring_Item_Flower3);
        itemSetActive.Add(itemType.FlowerSpringPlant4, Flower_Spring_Item_Flower4);
        itemSetActive.Add(itemType.FlowerSpringPlant5, Flower_Spring_Item_Flower5);
        itemSetActive.Add(itemType.FlowerSpringPlant6, Flower_Spring_Item_Flower6);
       // itemSetActive.Add(itemType.FlowerSummer, Flower_Summer_Item);
       // itemSetActive.Add(itemType.FlowerAutumn, Flower_Autumn_Item);
       // itemSetActive.Add(itemType.FlowerWinter, Flower_Winter_Item);
        itemSetActive.Add(itemType.Trap, Trap_Item);
        itemSetActive.Add(itemType.Shovel, Shovel_Item);
        //adding shovel automatically to inventory 
        inventoryList.Add(itemType.Shovel);
        selectItem = 1;

        DeactivateAll();
        NewItemSelected();
    }

    // Update is called once per frame
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
        inventoryList.Add(newItem);
        Debug.Log($"[Inventory] Added {newItem}. Inventory size: {inventoryList.Count}");

        // Auto-select the newly purchased item so it appears in the hotbar immediately
        selectItem = inventoryList.Count;
        NewItemSelected();
    }
    private void SelectSlot(int slot)
    {
        selectItem = slot;
        NewItemSelected();
    }

    private void NewItemSelected()
    {
        DeactivateAll();
        //Flower_Spring_Item_Flower1.SetActive(false);
        // Flower_Spring_Item_Flower2.SetActive(false);
        // Flower_Spring_Item_Flower3.SetActive(false);
        // Flower_Spring_Item_Flower4.SetActive(false);
        //Flower_Spring_Item_Flower5.SetActive(false);
        //Flower_Spring_Item_Flower6.SetActive(false);                        ||Most likely not needed but keeping incase||
        // Flower_Summer_Item.SetActive(false);
        //  Flower_Autumn_Item.SetActive(false);
        // Flower_Winter_Item.SetActive(false);
        //Trap_Item.SetActive(false);
        //Shovel_Item.SetActive(false);

        // Ensure selectItem is zero-indexed
        if (selectItem >= 1 && selectItem <= inventoryList.Count)
        {
            GameObject go = itemSetActive[inventoryList[selectItem - 1]];
            go.SetActive(true);
        }
        else if (inventoryList.Count > 0)
        {
            // Slot is out of range — just hold nothing (already deactivated)
            Debug.Log($"[Inventory] Slot {selectItem} is empty.");
        }
    }
    private void DeactivateAll()
    {
        foreach (var kvp in itemSetActive)
            kvp.Value.SetActive(false);
    }
}
