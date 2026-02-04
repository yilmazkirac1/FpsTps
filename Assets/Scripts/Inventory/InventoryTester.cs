using UnityEngine;

public class InventoryTester : MonoBehaviour
{
    public Inventory inventory;
    public ItemData testItem;

    void Start()
    {
        inventory.Add(testItem, 1);
    }
    void Update()
    {
     
            if (Input.GetKeyDown(KeyCode.I))
                inventory.Add(testItem, 1);
        

        if (Input.GetKeyDown(KeyCode.P))
            inventory.Print();
    }

}
