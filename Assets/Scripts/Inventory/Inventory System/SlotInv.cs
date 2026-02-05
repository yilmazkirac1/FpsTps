using System;

[Serializable]
public class SlotInv
{
    public ItemData Item;
    public int Amount;

    public bool IsEmpty()
    {
        return Item == null || Amount <= 0;

    }
    public void ClearSlot()
    {
        Item=null;
        Amount=0;
    }
}