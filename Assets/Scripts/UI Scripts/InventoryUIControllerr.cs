using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryUIControllerr : MonoBehaviour
{
    public DynamicInventoryDisplay chestPanel;
    public DynamicInventoryDisplay PlayerBackpackPanel;

    private void Awake() {
        chestPanel.gameObject.SetActive(false);
        PlayerBackpackPanel.gameObject.SetActive(false);
    }
    private void OnEnable() {
        InventoryHolder.OnDynamicInventoryDisplayRequested += DisplayInventory;
        PlayerInventoryHolder.OnPlayerBackpackDisplayRequested += DisplayPlayerBackpack;
    }

    private void OnDisable() {
        InventoryHolder.OnDynamicInventoryDisplayRequested += DisplayInventory;
        PlayerInventoryHolder.OnPlayerBackpackDisplayRequested -= DisplayPlayerBackpack;
    }
    void Update() {
        //if-ul asta s-ar putea sa fie redundant
        if (chestPanel.gameObject.activeInHierarchy && Keyboard.current.iKey.wasPressedThisFrame)
            chestPanel.gameObject.SetActive(false);

        if (PlayerBackpackPanel.gameObject.activeInHierarchy && Keyboard.current.iKey.wasPressedThisFrame) {
            PlayerBackpackPanel.gameObject.SetActive(false);
        }
    }

    void DisplayInventory(InventorySistem invToDisplay){
        chestPanel.gameObject.SetActive(true);
        chestPanel.RefreshDynamicInventory(invToDisplay);
    }
    void DisplayPlayerBackpack(InventorySistem invToDisplay){
        PlayerBackpackPanel.gameObject.SetActive(true);
        PlayerBackpackPanel.RefreshDynamicInventory(invToDisplay);
    }
}
