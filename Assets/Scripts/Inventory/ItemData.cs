using UnityEngine;

public enum ItemType { Weapon, Consumable, Key }

[CreateAssetMenu(menuName = "Items/Item")]
public class ItemData : ScriptableObject
{
    public string itemName;
    public ItemType type;
    public Sprite icon;
}
