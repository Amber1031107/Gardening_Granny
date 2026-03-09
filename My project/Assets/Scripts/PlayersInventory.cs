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
    [SerializeField] GameObject Flower_Spring_Item;
    [SerializeField] GameObject Flower_Summer_Item;
    [SerializeField] GameObject Flower_Autumn_Item;
    [SerializeField] GameObject Flower_Winter_Item;
    [SerializeField] GameObject Trap_Item;
    [SerializeField] GameObject Shovel_Item;

    private Dictionary<itemType, GameObject> itemSetActive = new Dictionary<itemType, GameObject>() { };
    private int selectedItem;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        itemSetActive.Add(itemType.FlowerSpring, Flower_Spring_Item);
        itemSetActive.Add(itemType.FlowerSummer, Flower_Summer_Item);
        itemSetActive.Add(itemType.FlowerAutumn, Flower_Autumn_Item);
        itemSetActive.Add(itemType.FlowerWinter, Flower_Winter_Item);
        itemSetActive.Add(itemType.Trap, Trap_Item);
        itemSetActive.Add(itemType.Shovel, Shovel_Item);

        NewItemSelected();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            selectItem = 1;
            NewItemSelected();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            selectItem = 2;
            NewItemSelected();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            selectItem = 3;
            NewItemSelected();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            selectItem = 4;
            NewItemSelected();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            selectItem = 5;
            NewItemSelected();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            selectItem = 6;
            NewItemSelected();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            selectItem = 7;
            NewItemSelected();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            selectItem = 8;
            NewItemSelected();
        }
    }

    private void NewItemSelected()
    {
        Flower_Spring_Item.SetActive(false);
        Flower_Summer_Item.SetActive(false);
        Flower_Autumn_Item.SetActive(false);
        Flower_Winter_Item.SetActive(false);
        Trap_Item.SetActive(false);
        Shovel_Item.SetActive(false);

        // Ensure selectItem is zero-indexed
        if (selectItem >= 1 && selectItem <= inventoryList.Count)
        {
            GameObject selectedItemGameObject = itemSetActive[inventoryList[selectItem - 1]];
            selectedItemGameObject.SetActive(true);
        }
        else
        {
            Debug.LogError("Invalid item selection: " + selectItem);
        }
    }
}
