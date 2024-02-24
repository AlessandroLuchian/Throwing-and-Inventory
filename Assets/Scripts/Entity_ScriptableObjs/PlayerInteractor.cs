using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerInteractor : MonoBehaviour
{
    [SerializeField] private PlayerData playerData;
    private GameObject playerObj;
    
    void Start() {
        //do note --> la game objects nu se foloseste getComponent<>
        this.playerObj = gameObject;
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
            gameObject.GetComponent<MeshRenderer>().material.color = Color.blue;
        }
        else if(this.playerData.currentHP<=74 && this.playerData.currentHP>=40) {
            gameObject.GetComponent<MeshRenderer>().material.color = Color.magenta;

        }
        else if(this.playerData.currentHP<=39 && this.playerData.currentHP>0) {
            gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
        }
        //add logic here that prevents you from healing at full hp
        else if(this.playerData.currentHP>100)
            this.playerData.currentHP = 100;
    }
}
