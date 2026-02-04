using TMPro;
using UnityEngine;

public class InventorySlotUI : MonoBehaviour
{
    public TMP_Text itemText;

    public void SetEmpty()
    {
        itemText.text = "-";
    }

    public void Set(ItemData item, int amount)
    {
        itemText.text = item.itemName + " x" + amount;
    }
}
