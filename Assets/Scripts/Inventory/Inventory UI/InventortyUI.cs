using UnityEngine;

public class InventortyUI : MonoBehaviour
{
    public InventorySystem InventorySystem;
    public GameObject[] SlotUIs;
    public GameObject Grid;

    public GameObject DragItemUIPrefab;
    public GameObject DragItemUI;

    private void Start()
    {
        for (int i = 0; i < SlotUIs.Length; i++)
        {
            SlotUIs[i].GetComponent<SlotUI>().SlotID = i;
        }
        ChangedAllSlots();
        InventorySystem.OnChangedInventory += ChangedAllSlots;

    }

    public void ChangedAllSlots()
    {
        for (int i = 0; i < SlotUIs.Length; i++)
        {
            SlotUIs[i].GetComponent<SlotUI>().ChangedSlotUI();
        }
    }

}
