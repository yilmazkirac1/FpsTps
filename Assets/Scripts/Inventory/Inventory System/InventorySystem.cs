using System;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    public SlotInv[] Slots=new SlotInv[25];

    public event Action OnChangedInventory;

    private void Awake()
    {
        for (int i = 0; i < Slots.Length; i++) 
        {
            if (Slots[i]==null)
                Slots[i] = new SlotInv();
        }
    }

    public void UpdateUI()
    {
        OnChangedInventory?.Invoke();
    }

    public void AddItem(ItemData item,int amount)
    {
        
        for (int i = 0; i < Slots.Length; i++)
        {           
            if (Slots[i].IsEmpty())
            {
                Slots[i].Item = item;  
                Slots[i].Amount = amount;
                UpdateUI();
                return;
            }
        }
        UpdateUI();
    }
    public void MoveItem( int currentIndex, int moveIndex)
    {
        if (!Slots[currentIndex].IsEmpty() && Slots[moveIndex].IsEmpty())
        {
            // Slots[moveIndex] = Slots[currentIndex];
             Slots[moveIndex].Item = Slots[currentIndex].Item;
             Slots[moveIndex].Amount = Slots[currentIndex].Amount;
            Slots[currentIndex].ClearSlot();
            UpdateUI();
        }
    }
}
