using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class shootingBehaviour : MonoBehaviour
{
    public void shoot(GunInventoryItemData gunData) {
        RaycastHit hit;
        if(Physics.Raycast(this.transform.position + new Vector3(0, 1f, 0), this.transform.forward, out hit)) {
            Debug.Log(hit.transform.name);

            target currentTarget = hit.transform.GetComponent<target>();
            if(currentTarget!=null) {
                currentTarget.takeDamage(gunData.damage);
            }
        }
    }
}