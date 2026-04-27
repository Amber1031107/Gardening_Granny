using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using AK.Wwise;

public class HotbarUI : MonoBehaviour
{
    [Header("References")]
    public PlayersInventory playerInventory;

    [Header("Fallback")]
    public Sprite icon_Default;

    [Header("Hotbar Slots (assign in Inspector)")]
    public List<HotbarSlot> slots;

    private int lastSelectedIndex = -1;

    private bool suppressNextSelectionSound = false; //Audio to stop switch sound when you place last item

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

    void CheckSelectionChanged()
    {
        if (playerInventory == null) return;

        if (playerInventory.selectItem != lastSelectedIndex)
        {
            lastSelectedIndex = playerInventory.selectItem;

            if (Shop.shopIsOpen) return;

            if (suppressNextSelectionSound) //Audio to stop switch sound when you place last item
            {
                suppressNextSelectionSound = false;
                return;
            }

            PlaySelectionSound();
        }
    }

    void PlaySelectionSound()
    {
        ItemData data = playerInventory.GetSelectedItemData();
        if (data == null) return;
        data.selectionSound?.Post(gameObject);
    }

    public void SuppressNextSelectionSound() //Audio to stop switch sound when you place last item
    {
        suppressNextSelectionSound = true;
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