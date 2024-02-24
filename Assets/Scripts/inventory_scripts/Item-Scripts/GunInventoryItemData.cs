using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//tav made changes here!
[CreateAssetMenu(menuName = "Inventory System/Gun Item")]
public class GunInventoryItemData : InventoryItemData {
    public float damage;
    public int magazineSize;
    public float timeToReload;
    public float fireRate;
    //!!!UNITY E FOARTE RETARDAT SI NU RESETEAZA LA 7 FIELDUL ASTA DE MAI JOS CAND DAI PLAY DIN NOU LA SCENA. 
    //DACA NU MAI TRAGE ARMA DE AICI E PROBLEMA!!! 
    public int currentAmountOfBulletsLoaded = 7;
    public ParticleSystem muzzleFlash;
    public TrailRenderer bulletTrail;

    public void decrementBulletsLoaded(int amountOfBulletsToDecrement) {
        this.currentAmountOfBulletsLoaded-=amountOfBulletsToDecrement;
    }

    public void reloadWeapon(int amountOfBulletsToReload) {
        this.currentAmountOfBulletsLoaded = amountOfBulletsToReload;
    }

    public bool canShoot() {
        if(this.currentAmountOfBulletsLoaded!=0)
            return true;
        return false;
    }
}