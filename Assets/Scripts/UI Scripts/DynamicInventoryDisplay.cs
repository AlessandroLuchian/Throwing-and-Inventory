using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class DynamicInventoryDisplay : InventoryDisplay
{
    [SerializeField] protected InventorySlot_UI slotPrefab;
    protected override void Start()
    {
        base.Start();
    }

    public void RefreshDynamicInventory(InventorySistem invToDisplay){
        ClearSlot();
        inventorySistem = invToDisplay;
        if(inventorySistem != null)
            inventorySistem.OnInventorySlotChanged += UpdateSlot;
        AssingSlot(invToDisplay);
    }

    public override void AssingSlot(InventorySistem invToDisplay)
    {
        slotDictionary = new Dictionary<InventorySlot_UI, InventorySlot>();

        if(invToDisplay == null)
            return;

        for(int i = 0; i < invToDisplay.InventorySize; i++){
            var uiSlot = Instantiate(slotPrefab, transform);
            slotDictionary.Add(uiSlot, invToDisplay.InventorySlots[i]);
            uiSlot.Init(invToDisplay.InventorySlots[i]);
            uiSlot.UpdateUISlot();
        }
    }
    
    private void ClearSlot(){
        foreach(var item in transform.Cast<Transform>()){
            // varianta mai buna este object pulling asta este doar un exemplu
            Destroy(item.gameObject);
        }

        if(slotDictionary != null)
            slotDictionary.Clear();
    }

    
    private void OnDisable() {
        if(inventorySistem != null)
            inventorySistem.OnInventorySlotChanged -= UpdateSlot;
    }
}
