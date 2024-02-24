using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

[RequireComponent(typeof(SphereCollider))]
public class ItemPickUp : MonoBehaviour
{
    public float PickUpRadius = 2f;
    public GameObject pickupIconPrefab;
    public InventoryItemData ItemData;
    private SphereCollider myCollider;
    [SerializeField] public string Comp1;
    [SerializeField] public string Comp2;
    private GameObject PressF;
    private GameObject Outline;

    private void Awake() {
        myCollider = GetComponent<SphereCollider>();
        myCollider.isTrigger = true;
        myCollider.radius = PickUpRadius;
        Outline = this.gameObject.transform.Find(Comp1).gameObject;
        PressF = this.gameObject.transform.Find(Comp2).gameObject;
        Outline.SetActive(false);
        PressF.SetActive(false);
    }

    private void OnTriggerStay(Collider other) {
        PressF.SetActive(true);
        Outline.SetActive(true);
        if(Input.GetKey(KeyCode.F)){
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
            Outline.SetActive(false);
    }
}
