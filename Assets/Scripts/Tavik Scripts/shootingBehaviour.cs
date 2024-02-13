using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class shootingBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject laser;
    private AudioSource audioSource;
    [SerializeField] private float fireRate = 15f;
    [SerializeField] private float damage;
    private float nextTimeToFire = 0;
    PlayerInventoryHolder InventorySystem;
    [SerializeField]
    InventoryItemData bulletData;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        InventorySystem = GetComponent<PlayerInventoryHolder>();
    }

    // Update is called once per frame
    void Update() {
        if(Input.GetMouseButton(1) && Input.GetMouseButtonDown(0) && Time.time >= nextTimeToFire && InventorySystem.CheckIfItemExists(bulletData)) {
            Debug.Log(InventorySystem.CheckIfItemExists(bulletData));
            nextTimeToFire = Time.time + 1f/fireRate;
            shoot();
            InventorySystem.RemoveFromInventory(bulletData, 1);
            if(!audioSource.isPlaying)
                audioSource.Play();
        }
        if(Input.GetMouseButton(1)) {
            laser.SetActive(true);
        }
        else
            laser.SetActive(false);
    }

    void shoot() {
        RaycastHit hit;
        if(Physics.Raycast(this.transform.position + new Vector3(0, 1f, 0), this.transform.forward, out hit)) {
            Debug.Log(hit.transform.name);
            if(!audioSource.isPlaying)
                audioSource.Play();

            target currentTarget = hit.transform.GetComponent<target>();
            if(currentTarget!=null) {
                currentTarget.takeDamage(damage);
            }
        }
    }
}