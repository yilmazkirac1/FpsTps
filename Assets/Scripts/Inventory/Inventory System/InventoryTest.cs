using UnityEngine;

public class InventoryTest : MonoBehaviour
{
    [SerializeField] private ItemData item;
    private InventorySystem inventorySystem;

    private void Start()
    {
        inventorySystem = GetComponent<InventorySystem>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            inventorySystem.AddItem(item, 1);
        }
    }
}
