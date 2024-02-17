using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    [SerializeField] private GameObject laser;
    private AudioSource audioSource;
    private float nextTimeToFire = 0;
    private PlayerInventoryHolder InventorySystem;
    [SerializeField]
    private InventoryItemData bulletData;
    [SerializeField]
    private GunInventoryItemData gunData;
    [SerializeField]
    private HealingInventoryData healingInventoryData;
    private shootingBehaviour shootingBehaviour;


    void Start() {
        this.InventorySystem = GetComponent<PlayerInventoryHolder>();
        this.shootingBehaviour = GetComponent<shootingBehaviour>();
        this.audioSource = GetComponent<AudioSource>();
    }

    void Update() {
         //TODO --> refactor this if InventorySystem.CheckIfItemExists(bulletData) may be redundant
        //if player is aiming down sights and if he has a gun type item and has bullets in inventory then he can shoot based on the fire rate of the gun that he is holding
        if(Input.GetMouseButton(1) && Input.GetMouseButtonDown(0) && Time.time >= nextTimeToFire && InventorySystem.CheckIfItemExists(gunData) && gunData.canShoot()) {
            nextTimeToFire = Time.time + 1f/gunData.fireRate;
            shootingBehaviour.shoot(gunData);
            //InventorySystem.RemoveFromInventory(bulletData, 1);
            gunData.decrementBulletsLoaded(1);
            Debug.Log("arma are " + gunData.currentAmountOfBulletsLoaded  + " gloante incarcate");
            Debug.Log("sunt in total " + InventorySystem.calculateNumberOfItemsPerSlot(bulletData) + " gloante in primul slot valabil");
            if(!audioSource.isPlaying)
                audioSource.Play();
        }

        if(Input.GetMouseButton(1) && InventorySystem.CheckIfItemExists(gunData)) 
            laser.SetActive(true);
        else
            laser.SetActive(false);

        if(Input.GetKeyDown(KeyCode.R)) {
            StartCoroutine(calculateHowToReload());
        }    

        if(Input.GetKeyDown(KeyCode.H)) {
            calculateHowToHeal();
        }
    }


     IEnumerator calculateHowToReload() {
        while(InventorySystem.CheckIfItemExists(bulletData) || gunData.currentAmountOfBulletsLoaded!=gunData.magazineSize) {
            if(gunData.magazineSize-gunData.currentAmountOfBulletsLoaded <= InventorySystem.calculateNumberOfItemsPerSlot(bulletData)) {
                Debug.Log("gloante de incarcat: " + (gunData.magazineSize-gunData.currentAmountOfBulletsLoaded));
                int bulletsToLoad = gunData.magazineSize-gunData.currentAmountOfBulletsLoaded;
                //gloante de incarcat = gloante incarcator - gloante incarcate
                gunData.currentAmountOfBulletsLoaded += bulletsToLoad;
                //gloante de scos din inventar = gloante de incarcat
                InventorySystem.RemoveFromInventory(bulletData, bulletsToLoad);
                InventorySystem.deleteNegativeItems(bulletData);
                yield return null;
            }
            else if (gunData.magazineSize-gunData.currentAmountOfBulletsLoaded >= InventorySystem.calculateNumberOfItemsPerSlot(bulletData) && InventorySystem.calculateNumberOfItemsPerSlot(bulletData)>0){
                Debug.Log("AM AJUNS AICI");
                int bulletsToLoad = InventorySystem.calculateNumberOfItemsPerSlot(bulletData);
                gunData.currentAmountOfBulletsLoaded += bulletsToLoad;
                InventorySystem.RemoveFromInventory(bulletData, bulletsToLoad);
                InventorySystem.deleteNegativeItems(bulletData);
                yield return null;
            }
            //wtf????? DO NOT REMOVE IT WILL CRASH
            //IDK WHY IT WORKS
            else 
                break;
        }
    }

    private void calculateHowToHeal() {
        Debug.Log("nr of healing items in 1st slot found: " + InventorySystem.calculateNumberOfItemsPerSlot(healingInventoryData));
    }

}
