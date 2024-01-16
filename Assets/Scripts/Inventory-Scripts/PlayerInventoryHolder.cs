using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class PlayerInventoryHolder : InventoryHolder
{

    [SerializeField] protected int backpackSize;
    [SerializeField] protected InventorySistem backpackInventorySistem;

    public InventorySistem BackpackInventorySistem => backpackInventorySistem;

    public static UnityAction<InventorySistem> OnPlayerBackpackDisplayRequested;

    protected override void Awake()
    {
        base.Awake();

        backpackInventorySistem = new InventorySistem(backpackSize);
    }
    void Update()
    {
        if(Keyboard.current.iKey.wasPressedThisFrame)
            OnPlayerBackpackDisplayRequested?.Invoke(backpackInventorySistem);
    }

    public bool AddToInventory(InventoryItemData data, int amount){
        if(primaryInventorySistem.AddToInventory(data, amount)){
            return true;
        }
        else if(backpackInventorySistem.AddToInventory(data, amount)){
            return true;
        }
        return false;
    }
}
