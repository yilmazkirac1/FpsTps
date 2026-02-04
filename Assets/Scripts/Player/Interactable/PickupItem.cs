using UnityEngine;

public class PickupItem : MonoBehaviour, IInteractable
{
    public ItemData item;
    public int amount = 1;

    public string GetPrompt()
    {
        return "E: Al " + item.itemName + " x" + amount;
    }

    public void Interact(GameObject interactor)
    {
        var inv = interactor.GetComponent<Inventory>();
        if (inv == null) return;

        if (inv.Add(item, amount))
        {
            Debug.Log("[Pickup] Picked up " + item.itemName + " x" + amount);
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("[Pickup] Inventory full!");
        }
    }
}
