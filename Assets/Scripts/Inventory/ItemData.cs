using UnityEngine;

public enum ItemType { Weapon, Wearable, Consumable, Key }
public enum EquipSlot { None, Weapon1, Weapon2, Boots, Armor, Gloves }

[CreateAssetMenu(menuName = "Items/Item")]
public class ItemData : ScriptableObject
{
    public string itemName;
    public ItemType type;
    public EquipSlot equipSlot; // Weapon ise Weapon1/2 olabilir, Wearable ise Boots/Armor/Gloves
    public Sprite icon;
}

