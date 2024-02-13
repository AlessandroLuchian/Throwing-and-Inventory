using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

[System.Serializable]
public class InventorySistem
{
    [SerializeField] private List<InventorySlot> inventorySlots;

    public List<InventorySlot> InventorySlots => inventorySlots;
    public int InventorySize => InventorySlots.Count;

    public UnityAction<InventorySlot> OnInventorySlotChanged;

    public InventorySistem(int size){
        inventorySlots = new List<InventorySlot>(size);

        for(int i = 0; i < size; i++){
            inventorySlots.Add(new InventorySlot());
        }
    }

    public bool AddToInventory(InventoryItemData itemToAdd, int amountToAdd){

        if(ContainsItem(itemToAdd, out List<InventorySlot> invSlot)){ // Check whether item exists in inventory
            foreach (var slot in invSlot){
                if(slot.RoomLeftInStack(amountToAdd)){
                    slot.AddToStack(amountToAdd);
                    OnInventorySlotChanged?.Invoke(slot);
                    return true;
                }
            }

        }
        if (HasFreeSlot(out InventorySlot freeSlot)){ // Gets the first available slot
            freeSlot.UdpateInventorySlot(itemToAdd, amountToAdd);
            OnInventorySlotChanged?.Invoke(freeSlot);
            return true;
        }

        return false;
    }

    //tav changes have been made here!
    public bool RemoveFromInventory(InventoryItemData itemToRemove, int amountToRemove) {
        if(ContainsItem(itemToRemove, out List<InventorySlot> invSlot)) {
            foreach(var slot in invSlot) {
                slot.RemoveFromStack(amountToRemove);
                OnInventorySlotChanged?.Invoke(slot);
                break;
            }
            return true;
        }
        return false;
    }

    public bool ContainsItem(InventoryItemData item, out List<InventorySlot> invSlot){
        invSlot = InventorySlots.Where(i => i.ItemData == item).ToList();
        Debug.Log(invSlot.Count);
        return invSlot.Any() ? true : false;
    }

    public bool HasFreeSlot(out InventorySlot freeSlot){
        freeSlot = InventorySlots.FirstOrDefault(i => i.ItemData == null);
        return freeSlot == null ? false : true;
    }
}
