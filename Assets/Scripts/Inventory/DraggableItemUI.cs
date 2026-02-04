using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableItemUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public ItemData item;
    private Transform originalParent;
    private Canvas canvas;
    public int fromIndex;
    public Inventory inventory;

    void Awake()
    {
        canvas = GetComponentInParent<Canvas>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalParent = transform.parent;

        var slotUI = originalParent.GetComponent<InventorySlotUI>();
        if (slotUI != null)
        {
            fromIndex = slotUI.slotIndex;
            inventory = slotUI.inventory;
        }

        transform.SetParent(canvas.transform);
    }



    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // Eðer bir yere býrakýlmadýysa eski yerine dönsün
        transform.SetParent(originalParent);
        transform.localPosition = Vector3.zero;
    }
}
