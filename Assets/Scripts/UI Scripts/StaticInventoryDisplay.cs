using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticInventoryDisplay : InventoryDisplay
{
    [SerializeField] private InventoryHolder inventoryHolder;
    [SerializeField] private InventorySlot_UI[] slots;
    protected override void Start()
    {
        base.Start();

        if(inventoryHolder != null){
            inventorySistem = inventoryHolder.InventorySistem;
            inventorySistem.OnInventorySlotChanged += UpdateSlot;
        }
        else
            Debug.Log($"No inventory assigned to {this.gameObject}");
        
        AssingSlot(inventorySistem);
    }
    public override void AssingSlot(InventorySistem invToDisplay)
    {
        slotDictionary = new Dictionary<InventorySlot_UI, InventorySlot>();

        if (slots.Length != inventorySistem.InventorySize)
            Debug.Log($"Inventory slots out of sync on {this.gameObject}");

        for(int i = 0; i < inventorySistem.InventorySize; i++){
            slotDictionary.Add(slots[i], inventorySistem.InventorySlots[i]);
            slots[i].Init(inventorySistem.InventorySlots[i]);
        }
    }


}
