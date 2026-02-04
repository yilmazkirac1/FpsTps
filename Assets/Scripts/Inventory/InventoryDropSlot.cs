using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryDropSlot : MonoBehaviour, IDropHandler
{
    public InventorySlotUI slotUI;

    void Awake()
    {
        if (slotUI == null) slotUI = GetComponent<InventorySlotUI>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        var drag = eventData.pointerDrag.GetComponent<DraggableItemUI>();
        if (drag == null || drag.inventory == null) return;

        int toIndex = slotUI.slotIndex;
        drag.inventory.Move(drag.fromIndex, toIndex);
    }
}
