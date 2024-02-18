using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerInteractor : MonoBehaviour
{
    [SerializeField] private PlayerData playerData;
    private GameObject playerObj;
    


    void Start() {
        this.playerObj = GetComponent<GameObject>();
    }

    void Update() {
        updateColorBasedOnHp();
    }

    private void OnTriggerEnter(Collider other) {
        
    }

    private void OnTriggerStay(Collider other) {
        
    }

    void takeDamage(float damageTaken) {
        this.playerData.currentHP -= damageTaken;
    }

    void updateColorBasedOnHp() {
        Debug.Log(this.playerObj);
        Debug.Log(this.playerData);
        if(this.playerData.currentHP<=100 && this.playerData.currentHP>=75) {

        }
        else if(this.playerData.currentHP<=74 && this.playerData.currentHP>=40) {

        }
        else if(this.playerData.currentHP<=39 && this.playerData.currentHP>0) {

        }
    }
}
