using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class InventoryHolder : MonoBehaviour
{
    [SerializeField] private int inventorySize;
    [SerializeField] protected InventorySistem inventorySistem;

    public InventorySistem InventorySistem => inventorySistem;

    public static UnityAction<InventorySistem> OnDynamicInventoryDisplayRequested;

    private void Awake() {
        inventorySistem = new InventorySistem(inventorySize);
    }
}
