using System;
using UnityEngine;

public class PlayerEquipment : MonoBehaviour
{
    public event Action OnChanged;

    public ItemData weapon1;
    public ItemData weapon2;

    public ItemData boots;
    public ItemData armor;
    public ItemData gloves;

    public bool TryEquip(ItemData item, EquipSlot slot)
    {
        if (item == null) return false;

        // Tip kontrolü
        if (slot == EquipSlot.Weapon1 || slot == EquipSlot.Weapon2)
        {
            if (item.type != ItemType.Weapon) return false;
        }
        else
        {
            if (item.type != ItemType.Wearable) return false;
        }

        // Ekip et
        switch (slot)
        {
            case EquipSlot.Weapon1: weapon1 = item; break;
            case EquipSlot.Weapon2: weapon2 = item; break;
            case EquipSlot.Boots: boots = item; break;
            case EquipSlot.Armor: armor = item; break;
            case EquipSlot.Gloves: gloves = item; break;
            default: return false;
        }

        if (OnChanged != null) OnChanged();
        return true;
    }

    public ItemData Get(EquipSlot slot)
    {
        switch (slot)
        {
            case EquipSlot.Weapon1: return weapon1;
            case EquipSlot.Weapon2: return weapon2;
            case EquipSlot.Boots: return boots;
            case EquipSlot.Armor: return armor;
            case EquipSlot.Gloves: return gloves;
        }
        return null;
    }
}
