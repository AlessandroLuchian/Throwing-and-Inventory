using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(SphereCollider))]
public class ItemPickUp : MonoBehaviour
{
    public float PickUpRadius = 1f;
    public GameObject pickupIconPrefab;
    public InventoryItemData ItemData;
    private SphereCollider myCollider;
    [SerializeField] public GameObject PressF;

    private void Awake() {
        myCollider = GetComponent<SphereCollider>();
        myCollider.isTrigger = true;
        myCollider.radius = PickUpRadius;
        PressF.SetActive(false);
    }


    private void OnTriggerStay(Collider other) {
        PressF.SetActive(true);
        if(Input.GetKeyDown(KeyCode.F)){
            var inventory = other.transform.GetComponent<PlayerInventoryHolder>();
            
            if(!inventory)
            return;

            if(inventory.AddToInventory(ItemData, 1)){
                Destroy(this.gameObject);
            }
        }
    }
    private void OnTriggerExit(Collider other) {
        PressF.SetActive(false);
    }
}
