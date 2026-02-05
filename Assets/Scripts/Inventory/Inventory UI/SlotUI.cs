using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class SlotUI : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public InventorySystem InventorySystem;
    public Transform InventoryPanel;
    public int SlotID;

    public GameObject Item;
    public Image Image;
    public TextMeshProUGUI Text;

    public GameObject DragItemUI;
    public GameObject _dragItemUI;
   
    public void ChangedSlotUI()
    {
        if (InventorySystem.Slots[SlotID].IsEmpty())
        {
            Item.SetActive(false);
            Image.sprite = null;
            Text.text = "";
        }
        else
        {
            Item.SetActive(true);
            Image.sprite = InventorySystem.Slots[SlotID].Item.Image;
            Text.text = InventorySystem.Slots[SlotID].Amount.ToString();
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (InventorySystem.Slots[SlotID].IsEmpty()) return;

        _dragItemUI = Instantiate(DragItemUI, InventoryPanel); 
        _dragItemUI.GetComponent<DragItemUI>().DragItem =  InventorySystem.Slots[SlotID].Item;
        _dragItemUI.GetComponent<DragItemUI>().DragAmount =  InventorySystem.Slots[SlotID].Amount;
        _dragItemUI.GetComponent<DragItemUI>().DragImage.sprite = Image.sprite;
        _dragItemUI.GetComponent<DragItemUI>().DragText.text = Text.text;
        _dragItemUI.GetComponent<DragItemUI>().BackIndex = SlotID;
        Item.SetActive(false);
        Image.sprite = null;
        Text.text = "";       

    }

    public void OnDrag(PointerEventData eventData)
    {
        _dragItemUI.transform.position=Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        SlotUI invTarget = GetInventorySlotUnderMouse(eventData.position);
        if (invTarget != null)
        {
            InventorySystem.MoveItem(SlotID, invTarget.SlotID);
            InventorySystem.UpdateUI();
            Destroy(_dragItemUI);
        }
        else
        {
            Item.SetActive(true);
            Image.sprite = _dragItemUI.GetComponent<DragItemUI>().DragImage.sprite;
            Text.text = _dragItemUI.GetComponent<DragItemUI>().DragAmount.ToString();
            Destroy(_dragItemUI);
        }

        //nereye biraktigimi ogrenmeliyim
        //birakmaya calistigim yer bir slotmu
        //slotsa bu slot dolumu bosmu
        //doluysa swap bossa move
        //dragitem 
    }
    SlotUI GetInventorySlotUnderMouse(Vector2 screenPos)
    {
        var results = RaycastAll(screenPos);

        foreach (var r in results)
        {
            var s = r.gameObject.GetComponentInParent<SlotUI>();
            if (s != null) return s;
        }
        return null;
    }
    List<RaycastResult> RaycastAll(Vector2 screenPos)
    {
        var results = new List<RaycastResult>();

        if (EventSystem.current == null) return results;

        var data = new PointerEventData(EventSystem.current)
        {
            position = screenPos
        };

        EventSystem.current.RaycastAll(data, results);
        return results;
    }
}
