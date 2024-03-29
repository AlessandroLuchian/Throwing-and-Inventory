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
    public void SlotClicked(InventorySlot_UI clickedUISlot){
        // Clicked slot has an item - mouse doesn't have an item - pick up that item.

        bool isAltPressed = Keyboard.current.leftAltKey.isPressed;

        if(clickedUISlot.AssignedInventorySlot.ItemData != null && mouseInventoryItem.AssignedInventorySlot.ItemData == null){
            // if player holding ALT key? Split the stack.
            if(isAltPressed && clickedUISlot.AssignedInventorySlot.SplitStack(out InventorySlot halfStackSlot)){
                mouseInventoryItem.UpdateMouseSlot(halfStackSlot);
                clickedUISlot.UpdateUISlot();
                return;
            }
            else{
                mouseInventoryItem.UpdateMouseSlot(clickedUISlot.AssignedInventorySlot);
                clickedUISlot.ClearSlot();
                return;
            }
        }

        // Clicked slot doesn't have an item - Mouse does have an item - place the mouse item into the empty slot.
        if(clickedUISlot.AssignedInventorySlot.ItemData == null && mouseInventoryItem.AssignedInventorySlot.ItemData != null){

            clickedUISlot.AssignedInventorySlot.AssignItem(mouseInventoryItem.AssignedInventorySlot);
            clickedUISlot.UpdateUISlot();

            mouseInventoryItem.ClearSlot();
        }

        // Both slots have an item - decide what to do...
            // Are both items the same? if so combine them.
                // is slot stack size + mouse stack size > the slot Max Stack size? if so, take from mouse.
            // If different items, then swap the items.
            // Both slots have an item - decide what to do...
            if(clickedUISlot.AssignedInventorySlot.ItemData != null && mouseInventoryItem.AssignedInventorySlot.ItemData != null){
                
                bool isSameItem = clickedUISlot.AssignedInventorySlot.ItemData == mouseInventoryItem.AssignedInventorySlot.ItemData;

                if(isSameItem && clickedUISlot.AssignedInventorySlot.RoomLeftInStack(mouseInventoryItem.AssignedInventorySlot.StackSize)){   
                    clickedUISlot.AssignedInventorySlot.AssignItem(mouseInventoryItem.AssignedInventorySlot);
                    clickedUISlot.UpdateUISlot();

                    mouseInventoryItem.ClearSlot();
                }
                else if(isSameItem && !clickedUISlot.AssignedInventorySlot.RoomLeftInStack(mouseInventoryItem.AssignedInventorySlot.StackSize, out int leftInStack)){
                    if(leftInStack < 1)
                        SwapSlots(clickedUISlot); // Stack is full so swap the items.
                    else{ 
                        //Slot is not at max, so take what needed from the mouse inventory.
                        int remainingOnMouse = mouseInventoryItem.AssignedInventorySlot.StackSize - leftInStack;
                        clickedUISlot.AssignedInventorySlot.AddToStack(leftInStack);
                        clickedUISlot.UpdateUISlot();

                        var newItem = new InventorySlot(mouseInventoryItem.AssignedInventorySlot.ItemData, remainingOnMouse);
                        mouseInventoryItem.ClearSlot();
                        mouseInventoryItem.UpdateMouseSlot(newItem);
                        return;
                    }
                }
                else if(!isSameItem){
                    SwapSlots(clickedUISlot);
                    return;
                }
            }
    }

    private void SwapSlots(InventorySlot_UI clickedUISlot){
        var clonedSlot = new InventorySlot(mouseInventoryItem.AssignedInventorySlot.ItemData, mouseInventoryItem.AssignedInventorySlot.StackSize);
        mouseInventoryItem.ClearSlot();

        mouseInventoryItem.UpdateMouseSlot(clickedUISlot.AssignedInventorySlot);

        clickedUISlot.ClearSlot();
        clickedUISlot.AssignedInventorySlot.AssignItem(clonedSlot);
        clickedUISlot.UpdateUISlot();
    }
}
