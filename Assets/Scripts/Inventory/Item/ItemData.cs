using UnityEngine;

public enum ItemType
{
    None,
    Consumable,
    Material,
    Armor,
    Weapon
}
public enum WeaponType
{
    None,
    Weapon_1,
    Weapon_2,
    Armor,
    Gloves,
    Boots,
    Helmet,
    Belt
}
[CreateAssetMenu(fileName ="Item",menuName = "Inventory/Item")]
public class ItemData : ScriptableObject
{
    public string Id;
    public string Name;
    public string Desc;
    public Sprite Image;
    public bool IsStacable;
    public int MaxStackCount;
    public ItemType ItemType;
    public WeaponType WeaponType;
}
