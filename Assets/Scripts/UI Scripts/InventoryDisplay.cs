using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public abstract class InventoryDisplay : MonoBehaviour
{
    [SerializeField] MouseItemData mouseInventoryItem;
    protected InventorySistem inventorySistem;
    protected Dictionary<InventorySlot_UI, InventorySlot> slotDictionary;
    public InventorySistem InventorySistem => inventorySistem;
    public Dictionary<InventorySlot_UI, InventorySlot> SlotDictionary => slotDictionary;

    protected virtual void Start() {
        
    } 

    public virtual void AssingSlot(InventorySistem invToDisplay){
        
    }

    protected virtual void UpdateSlot(InventorySlot updatedSlot){
        foreach (var slot in SlotDictionary){

            if(slot.Value == updatedSlot){ // Slot value - the inventory slot

                slot.Key.UpdateUISlot(updatedSlot); // Slot key - the UI represantation of the value

            }
        }
    }
    public void SlotClicked(InventorySlot_UI clickedSlot){
        Debug.Log("Slot clicked");
    }
}
