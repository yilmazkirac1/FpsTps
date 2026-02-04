using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EquipSlotUI : MonoBehaviour, IDropHandler
{
    public EquipSlot slot;
    public PlayerEquipment equipment;
    public Image iconImage;

    public void OnDrop(PointerEventData eventData)
    {
        var drag = eventData.pointerDrag.GetComponent<DraggableItemUI>();
        if (drag == null || drag.item == null) return;

        bool ok = equipment.TryEquip(drag.item, slot);
        if (!ok) return;

        // ikon güncelle
        if (drag.item.icon != null)
        {
            iconImage.sprite = drag.item.icon;
            iconImage.enabled = true;
        }

        // Ýstersen: item inventory’den düþsün (bunu bir sonraki adýmda baðlarýz)
        // Þimdilik sadece equip ediyoruz.
    }
}
