using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class shootingBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject laser;
    private AudioSource audioSource;
    private float nextTimeToFire = 0;
    PlayerInventoryHolder InventorySystem;
    [SerializeField]
    InventoryItemData bulletData;
    [SerializeField]
    GunInventoryItemData gunData;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        InventorySystem = GetComponent<PlayerInventoryHolder>();
    }

    void Update() {
        //TODO --> refactor this if InventorySystem.CheckIfItemExists(bulletData) may be redundant
        //if player is aiming down sights and if he has a gun type item and has bullets in inventory then he can shoot based on the fire rate of the gun that he is holding
        if(Input.GetMouseButton(1) && Input.GetMouseButtonDown(0) && Time.time >= nextTimeToFire && InventorySystem.CheckIfItemExists(gunData) && gunData.canShoot()) {
            nextTimeToFire = Time.time + 1f/gunData.fireRate;
            shoot();
            //InventorySystem.RemoveFromInventory(bulletData, 1);
            gunData.decrementBulletsLoaded(1);
            Debug.Log("arma are " + gunData.currentAmountOfBulletsLoaded  + " gloante incarcate");
            Debug.Log("sunt in total " + InventorySystem.calculateNumberOfItems(bulletData) + " gloante in primul slot valabil");
            if(!audioSource.isPlaying)
                audioSource.Play();
        }

        if(Input.GetKeyDown(KeyCode.R)) {
            if(gunData.currentAmountOfBulletsLoaded == gunData.magazineSize)
                return;
            else {
                //calculateNumberOfItems returneaza numarul de iteme din PRIMUL SLOT IN CARE GASESTE ITEM!!
                if(InventorySystem.calculateNumberOfItems(bulletData)>0){
                    //daca am mai multe gloante in slot decat am nevoie ca sa incarc 
                    if(gunData.magazineSize-gunData.currentAmountOfBulletsLoaded <= InventorySystem.calculateNumberOfItems(bulletData)) {
                        Debug.Log("gloante de incarcat: " + (gunData.magazineSize-gunData.currentAmountOfBulletsLoaded));
                        int bulletsToLoad = gunData.magazineSize-gunData.currentAmountOfBulletsLoaded;
                        //gloante de incarcat = gloante incarcator - gloante incarcate
                        gunData.currentAmountOfBulletsLoaded += bulletsToLoad;
                        //gloante de scos din inventar = gloante de incarcat
                        InventorySystem.RemoveFromInventory(bulletData, bulletsToLoad);
                    }
                    else if (InventorySystem.calculateNumberOfItems(bulletData) < gunData.magazineSize-gunData.currentAmountOfBulletsLoaded) {
                        Debug.Log("AM AJUNS AICI");
                        int bulletsToLoad = InventorySystem.calculateNumberOfItems(bulletData);
                        gunData.currentAmountOfBulletsLoaded += bulletsToLoad;
                        InventorySystem.RemoveFromInventory(bulletData, bulletsToLoad);
                        InventorySystem.deleteNegativeItems(bulletData);
                    }
                }
               
            }
     
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
                currentTarget.takeDamage(gunData.damage);
            }
        }
    }
}