using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class PlayerInventoryHolder : InventoryHolder
{

    [SerializeField] public int backpackSize;
    [SerializeField] public InventorySistem backpackInventorySistem;
    [SerializeField] public DynamicInventoryDisplay dynamicInventoryDisplay;
    public DynamicInventoryDisplay DynamicInventoryDisplay => dynamicInventoryDisplay;

    public InventorySistem BackpackInventorySistem => backpackInventorySistem;

    public static UnityAction<InventorySistem> OnPlayerBackpackDisplayRequested;
    bool isInInventory = false;

    protected override void Awake()
    {
        base.Awake();

        backpackInventorySistem = new InventorySistem(backpackSize);
    }
    
    //uita-te pe InventoryUIcontroller ca sa vezi exact cum intractioneaza
    //cu timpul incearca sa muti asta in InputHandler...
    void Update() {
        if(Keyboard.current.iKey.wasPressedThisFrame && isInInventory == true) {
            Debug.Log("am returnat");
            isInInventory = false;
            return;
        }
        else if(Keyboard.current.iKey.wasPressedThisFrame) {
            OnPlayerBackpackDisplayRequested?.Invoke(backpackInventorySistem);
            isInInventory = true;
            Debug.Log("este in inventar: " + isInInventory);
        }

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
    
    //tav made changes here
    public bool RemoveFromInventory(InventoryItemData data, int amount) {
        if(backpackInventorySistem.RemoveFromInventory(data, amount)) {
            return true;
        }
        return false;
    }

    public bool CheckIfItemExists(InventoryItemData data) {
        if(backpackInventorySistem.ContainsItem(data, out List<InventorySlot> invSlot)) 
            return true;
        return false;
    }

    public int calculateNumberOfItemsPerSlot(InventoryItemData data) {
        return backpackInventorySistem.calculateNumberOfItemsPerSlot(data);
    }

    public void deleteNegativeItems(InventoryItemData data) {
        backpackInventorySistem.deleteSlot(data);
    }
}
