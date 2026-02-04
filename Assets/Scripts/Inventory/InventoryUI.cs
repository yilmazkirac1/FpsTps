using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public Inventory inventory;
    public GameObject panel;
    public Transform slotsParent;
    public InventorySlotUI slotPrefab;

    private InventorySlotUI[] uiSlots;
    void Start()
    {
        Debug.Log("[InventoryUI] Start called");

        // UI slotlarýný üret
        uiSlots = new InventorySlotUI[inventory.slots.Count];
        for (int i = 0; i < inventory.slots.Count; i++)
        {
            uiSlots[i] = Instantiate(slotPrefab, slotsParent, false);
        }

        // EN ÖNEMLÝ SATIR
        inventory.OnChanged += Refresh;
        Debug.Log("[InventoryUI] Subscribed to OnChanged");

        Refresh();

        panel.SetActive(false);
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            bool newState = !panel.activeSelf;
            panel.SetActive(newState);
            Debug.Log("[InventoryUI] Panel set to: " + newState);
        }
    }

    void Refresh()
    {
        for (int i = 0; i < inventory.slots.Count; i++)
        {
            var s = inventory.slots[i];

            if (s.IsEmpty) uiSlots[i].SetEmpty();
            else uiSlots[i].Set(s.item, s.amount);
        }
    }
}
