using System.Collections.Generic;
using UnityEngine;

public class PlayersInventory : MonoBehaviour
{
    [Header("General")]
    public List<string> inventoryList = new List<string>();
    public Dictionary<string, int> itemCounts = new Dictionary<string, int>();
    public int selectItem;

    private HotbarUI hotbarUI; //Audio to stop switch sound when you place last item

    private const int MAX_VISIBLE = 8;
    private int windowStart = 0;

    bool shovelAdded = false;

    public int GetWindowStart() => windowStart;

    [Header("Item Registry — drag all ItemData SOs here")]
    [SerializeField] List<ItemData> itemRegistry;

    private Dictionary<string, ItemData> itemDataMap = new Dictionary<string, ItemData>();
    private Dictionary<string, GameObject> spawnedHandItems = new Dictionary<string, GameObject>();

    [Header("Hand anchor")]
    [SerializeField] Transform handAnchor;

    void Start()
    {
        hotbarUI = FindObjectOfType<HotbarUI>(); //Audio to stop switch sound when you place last item

        foreach (var data in itemRegistry)
        {
            if (string.IsNullOrEmpty(data.itemID))
            {
                Debug.LogWarning($"[Inventory] ItemData asset '{data.name}' has no itemID — skipping.");
                continue;
            }
            if (!itemDataMap.ContainsKey(data.itemID))
                itemDataMap[data.itemID] = data;
            else
                Debug.LogWarning($"[Inventory] Duplicate itemID '{data.itemID}' — skipping.");
        }

        // Add shovel — find it by the isShovel flag, no hardcoded ID needed
        foreach (var data in itemRegistry)
        {
            if (data.isShovel)
            {
                if (string.IsNullOrEmpty(data.itemID))
                {
                    Debug.LogError($"[Inventory] Shovel ItemData '{data.name}' has no itemID set — shovel not added.");
                    break;
                }
                inventoryList.Add(data.itemID);
                itemCounts[data.itemID] = -1;
                shovelAdded = true;
                Debug.Log($"[Inventory] Shovel added with ID '{data.itemID}'");
                break;
            }
        }

        if (!shovelAdded)
            Debug.LogError("[Inventory] No ItemData with isShovel = true found in registry. Did you forget to add it or tick the flag?");

        selectItem = 1;
        DeactivateAll();
        NewItemSelected();
    }

    void Update()
    {
        // ── Number keys ───────────────────────────────────────────────────────
        int visibleCount = Mathf.Min(MAX_VISIBLE, inventoryList.Count - windowStart);
        for (int i = 1; i <= visibleCount; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha0 + i))
            {
                SelectSlot(windowStart + i);
                break;
            }
        }

        // ── Scroll wheel ──────────────────────────────────────────────────────
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0f)
        {
            // Scrolling down = move right, scrolling up = move left
            int direction = scroll < 0f ? 1 : -1;
            int newIndex = Mathf.Clamp((selectItem - 1) + direction, 0, inventoryList.Count - 1);

            selectItem = newIndex + 1;
            ScrollWindowToIndex(newIndex);
            NewItemSelected();
        }
    }

    // ── Public API ────────────────────────────────────────────────────────────

    public void AddItem(string itemID)
    {
        if (!itemDataMap.ContainsKey(itemID))
        {
            Debug.LogWarning($"[Inventory] Unknown itemID '{itemID}'.");
            return;
        }

        if (itemCounts.ContainsKey(itemID))
        {
            itemCounts[itemID]++;
            Debug.Log($"[Inventory] {itemID} stacked to {itemCounts[itemID]}");
        }
        else
        {
            inventoryList.Add(itemID);
            itemCounts[itemID] = 1;
        }

        int newIndex = inventoryList.IndexOf(itemID);

        bool isVisible = newIndex >= windowStart && newIndex < windowStart + MAX_VISIBLE;
        if (isVisible)
        {
            selectItem = newIndex + 1;
            NewItemSelected();
        }
        else
        {
            // Just update counts/state without moving the window or selection
            NewItemSelected();
        }
    }

    public void ConsumeItem(string itemID)
    {
        if (!itemCounts.ContainsKey(itemID)) return;
        if (itemCounts[itemID] == -1) return;

        itemCounts[itemID]--;

        if (itemCounts[itemID] <= 0)
        {
            if (hotbarUI != null)
                hotbarUI.SuppressNextSelectionSound(); //Audio to stop switch sound when you place last item

            inventoryList.Remove(itemID);
            itemCounts.Remove(itemID);

            if (spawnedHandItems.TryGetValue(itemID, out GameObject old))
            {
                Destroy(old);
                spawnedHandItems.Remove(itemID);
            }

            if (windowStart > 0 && windowStart + MAX_VISIBLE > inventoryList.Count)
                windowStart = Mathf.Max(0, inventoryList.Count - MAX_VISIBLE);

            if (selectItem > inventoryList.Count)
                selectItem = Mathf.Max(1, inventoryList.Count);

            if (inventoryList.Count > 0)
                selectItem = Mathf.Clamp(selectItem, windowStart + 1, windowStart + MAX_VISIBLE);
        }

        NewItemSelected();
    }

    public string GetSelectedItemID()
    {
        int index = selectItem - 1;
        if (index < 0 || index >= inventoryList.Count) return null;
        return inventoryList[index];
    }

    public ItemData GetSelectedItemData()
    {
        string id = GetSelectedItemID();
        if (id == null) return null;
        return itemDataMap.TryGetValue(id, out ItemData d) ? d : null;
    }

    public ItemData GetItemData(string itemID)
    {
        return itemDataMap.TryGetValue(itemID, out ItemData d) ? d : null;
    }

    public int GetSelectedItemCount()
    {
        string id = GetSelectedItemID();
        if (id == null) return 0;
        return itemCounts.TryGetValue(id, out int count) ? count : 0;
    }

    public int GetSelectedVisualSlot()
    {
        int index = selectItem - 1;
        int visualSlot = index - windowStart + 1;
        return (visualSlot >= 1 && visualSlot <= MAX_VISIBLE) ? visualSlot : -1;
    }

    // ── Internals ─────────────────────────────────────────────────────────────

    private void SelectSlot(int slot)
    {
        selectItem = slot;
        NewItemSelected();
    }

    private void NewItemSelected()
    {
        DeactivateAll();

        string id = GetSelectedItemID();
        if (id == null) return;

        if (!itemDataMap.TryGetValue(id, out ItemData data)) return;

        if (!spawnedHandItems.TryGetValue(id, out GameObject handObj) || handObj == null)
        {
            if (data.handPrefab == null) return;
            handObj = Instantiate(data.handPrefab, handAnchor);
            spawnedHandItems[id] = handObj;
        }

        handObj.SetActive(true);
    }

    private void DeactivateAll()
    {
        foreach (var kvp in spawnedHandItems)
            if (kvp.Value != null)
                kvp.Value.SetActive(false);
    }

    private void ScrollWindowToIndex(int index)
    {
        if (index < windowStart)
            windowStart = index;
        else if (index >= windowStart + MAX_VISIBLE)
            windowStart = index - MAX_VISIBLE + 1;
    }
}