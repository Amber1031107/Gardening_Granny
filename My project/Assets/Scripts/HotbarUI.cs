using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HotbarUI : MonoBehaviour
{
    [Header("References")]
    public PlayersInventory playerInventory;

    [Header("Item Icons — drag each sprite to match its item type")]
    public Sprite icon_FlowerSpringPlant1;
    public Sprite icon_FlowerSpringPlant2;
    public Sprite icon_FlowerSpringPlant3;
    public Sprite icon_FlowerSpringPlant4;
    public Sprite icon_FlowerSpringPlant5;
    public Sprite icon_FlowerSpringPlant6;
    public Sprite icon_Trap;
    public Sprite icon_Shovel;
    public Sprite icon_Default; // fallback if no icon is assigned

    [Header("Hotbar Slots (assign in Inspector — one per slot)")]
    public List<HotbarSlot> slots;

    private Dictionary<itemType, Sprite> iconMap;

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

    void Update()
    {
        RefreshHotbar();
    }

    void RefreshHotbar()
    {
        for (int i = 0; i < slots.Count; i++)
        {
            bool hasItem = i < playerInventory.inventoryList.Count;
            bool selected = playerInventory.selectItem == i + 1;

            if (hasItem)
            {
                itemType item = playerInventory.inventoryList[i];

                int count = playerInventory.itemCounts.ContainsKey(item)
                    ? playerInventory.itemCounts[item]
                    : 0;

                Sprite icon = iconMap.ContainsKey(item) && iconMap[item] != null
                    ? iconMap[item]
                    : icon_Default;

                slots[i].SetSlot(item, icon, count, selected);
            }
            else
            {
                slots[i].ClearSlot();
            }
        }
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

    public void SetSlot(itemType item, Sprite icon, int count, bool selected)
    {
        slotObject.SetActive(true);

        // Set the icon sprite
        if (iconImage != null)
        {
            iconImage.sprite = icon;
            iconImage.enabled = icon != null;
        }

        // Stack count — blank for infinite (shovel = -1)
        if (countText != null)
            countText.text = count == -1 ? "" : "x" + count.ToString();

        // Friendly name
        if (nameText != null)
            nameText.text = FormatName(item.ToString());

        // Highlight selected slot
        if (selectionHighlight != null)
            selectionHighlight.enabled = selected;
    }

    public void ClearSlot()
    {
        slotObject.SetActive(false);
    }

    private string FormatName(string raw)
    {
        raw = raw.Replace("FlowerSpring", "Spring ");
        raw = raw.Replace("FlowerSummer", "Summer ");
        raw = raw.Replace("FlowerAutumn", "Autumn ");
        raw = raw.Replace("FlowerWinter", "Winter ");

        // Insert space before digits: "Plant1" → "Plant 1"
        var sb = new System.Text.StringBuilder();
        foreach (char c in raw)
        {
            if (char.IsDigit(c) && sb.Length > 0 && !char.IsDigit(sb[sb.Length - 1]))
                sb.Append(' ');
            sb.Append(c);
        }
        return sb.ToString().Trim();
    }
}
