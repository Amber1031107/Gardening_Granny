using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
<<<<<<< HEAD
using AK.Wwise; 
=======
using AK.Wwise;
>>>>>>> MaybeBetterFixForPolishingPurposes

public class HotbarUI : MonoBehaviour
{
    [Header("References")]
    public PlayersInventory playerInventory;

    [Header("Fallback")]
    public Sprite icon_Default;

    [Header("Hotbar Slots (assign in Inspector)")]
    public List<HotbarSlot> slots;

<<<<<<< HEAD
    [Header("Selection Audio")] //Audio
    public AK.Wwise.Event select_FlowerSpringPlant1;
    public AK.Wwise.Event select_FlowerSpringPlant2;
    public AK.Wwise.Event select_FlowerSpringPlant3;
    public AK.Wwise.Event select_FlowerSpringPlant4;
    public AK.Wwise.Event select_FlowerSpringPlant5;
    public AK.Wwise.Event select_FlowerSpringPlant6;
    public AK.Wwise.Event select_Trap;
    public AK.Wwise.Event select_Shovel;
    public AK.Wwise.Event select_Default;

    private Dictionary<itemType, Sprite> iconMap;
    private int lastSelectedIndex = -1;


    void Start()
    {
        // Build the icon lookup once
        iconMap = new Dictionary<itemType, Sprite>()
        {
            { itemType.FlowerSpringPlant1, icon_FlowerSpringPlant1 },
            { itemType.FlowerSpringPlant2, icon_FlowerSpringPlant2 },
            { itemType.FlowerSpringPlant3, icon_FlowerSpringPlant3 },
            { itemType.FlowerSpringPlant4, icon_FlowerSpringPlant4 },
            { itemType.FlowerSpringPlant5, icon_FlowerSpringPlant5 },
            { itemType.FlowerSpringPlant6, icon_FlowerSpringPlant6 },
            { itemType.Trap,              icon_Trap },
            { itemType.Shovel,            icon_Shovel },
        };
    }
=======
    private int lastSelectedIndex = -1;
>>>>>>> MaybeBetterFixForPolishingPurposes

    void Update()
    {
        RefreshHotbar();
        CheckSelectionChanged();
    }

    void RefreshHotbar()
    {
        // windowStart tells us which inventory index maps to slot 1
        int windowStart = playerInventory.GetWindowStart();

        for (int i = 0; i < slots.Count; i++)
        {
            int inventoryIndex = windowStart + i;
            bool hasItem = inventoryIndex < playerInventory.inventoryList.Count;
            bool selected = playerInventory.selectItem == inventoryIndex + 1;

            if (hasItem)
            {
                string itemID = playerInventory.inventoryList[inventoryIndex];
                ItemData data = playerInventory.GetItemData(itemID);

                int count = playerInventory.itemCounts.ContainsKey(itemID)
                    ? playerInventory.itemCounts[itemID]
                    : 0;

                Sprite icon = (data != null && data.icon != null) ? data.icon : icon_Default;
                string displayName = data != null ? data.displayName : itemID;

                slots[i].SetSlot(displayName, icon, count, selected);
            }
            else
            {
                slots[i].ClearSlot();
            }
        }
    }

<<<<<<< HEAD

   void CheckSelectionChanged()
    {
        if (playerInventory == null)
            return;
=======
    void CheckSelectionChanged()
    {
        if (playerInventory == null) return;
>>>>>>> MaybeBetterFixForPolishingPurposes

        if (playerInventory.selectItem != lastSelectedIndex)
        {
            lastSelectedIndex = playerInventory.selectItem;

<<<<<<< HEAD
            // Don't play hotbar swap sound while shop is open
            if (Shop.shopIsOpen)
                return;
=======
            if (Shop.shopIsOpen) return;
>>>>>>> MaybeBetterFixForPolishingPurposes

            PlaySelectionSound();
        }
    }

    void PlaySelectionSound()
    {
<<<<<<< HEAD
        int selectedIndex = playerInventory.selectItem - 1;

        if (selectedIndex < 0 || selectedIndex >= playerInventory.inventoryList.Count)
            return;

        itemType selectedItem = playerInventory.inventoryList[selectedIndex];
        AK.Wwise.Event selectionEvent = GetSelectionEvent(selectedItem);

        selectionEvent?.Post(gameObject);
    }

    AK.Wwise.Event GetSelectionEvent(itemType item)
    {
        switch (item)
        {
            case itemType.FlowerSpringPlant1: return select_FlowerSpringPlant1;
            case itemType.FlowerSpringPlant2: return select_FlowerSpringPlant2;
            case itemType.FlowerSpringPlant3: return select_FlowerSpringPlant3;
            case itemType.FlowerSpringPlant4: return select_FlowerSpringPlant4;
            case itemType.FlowerSpringPlant5: return select_FlowerSpringPlant5;
            case itemType.FlowerSpringPlant6: return select_FlowerSpringPlant6;
            case itemType.Trap: return select_Trap;
            case itemType.Shovel: return select_Shovel;
            default: return select_Default;
        }
=======
        ItemData data = playerInventory.GetSelectedItemData();
        if (data == null) return;
        data.selectionSound?.Post(gameObject);
>>>>>>> MaybeBetterFixForPolishingPurposes
    }
}


[System.Serializable]
public class HotbarSlot
{
    public GameObject slotObject;
    public Image iconImage;
    public TextMeshProUGUI countText;
    public TextMeshProUGUI nameText;
    public Image selectionHighlight;

    public void SetSlot(string displayName, Sprite icon, int count, bool selected)
    {
        slotObject.SetActive(true);

        if (iconImage != null)
        {
            iconImage.sprite = icon;
            iconImage.enabled = icon != null;
        }

        if (countText != null)
            countText.text = count == -1 ? "" : "x" + count;

        if (nameText != null)
            nameText.text = displayName;

        if (selectionHighlight != null)
            selectionHighlight.enabled = selected;
    }

    public void ClearSlot()
    {
        slotObject.SetActive(false);
    }
}