using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class InventoryHolder : MonoBehaviour
{
    [SerializeField] private int inventorySize;
    [SerializeField] protected InventorySistem primaryInventorySistem;

    public InventorySistem PrimaryInventorySistem => primaryInventorySistem;

    public static UnityAction<InventorySistem> OnDynamicInventoryDisplayRequested;

    protected virtual void Awake(){
        primaryInventorySistem = new InventorySistem(inventorySize);
    }
}
