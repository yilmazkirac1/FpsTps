using System.Collections.Generic;
using UnityEngine;
using System;


public class Inventory : MonoBehaviour
{
    public List<InventorySlot> slots = new List<InventorySlot>(8);
    public event Action OnChanged;
    // Add fonksiyonunun baþarýlý olduðu her yerde en sona bunu ekle:
    // OnChanged?.Invoke();
    void Awake()
    {
        for (int i = 0; i < 8; i++)
            slots.Add(new InventorySlot());
    }

    public bool Add(ItemData item, int amount = 1)
    {
        // ayný item varsa
        foreach (var slot in slots)
        {
            if (slot.item == item)
            {
                slot.amount += amount;
                if (OnChanged != null) OnChanged();
                return true;
            }
        }

        // boþ slot
        foreach (var slot in slots)
        {
            if (slot.IsEmpty)
            {
                slot.item = item;
                slot.amount = amount;
                if (OnChanged != null) OnChanged();
                return true;
            }
        }

        return false;
    }

    public void Print()
    {
        for (int i = 0; i < slots.Count; i++)
        {
            var s = slots[i];
            if (s.IsEmpty) Debug.Log($"Slot {i}: EMPTY");
            else Debug.Log($"Slot {i}: {s.item.itemName} x{s.amount}");
        }
    }

}
