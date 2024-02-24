using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class shootingBehaviour : MonoBehaviour
{
    public void shoot(GunInventoryItemData gunData) {
        RaycastHit hit;
        //raycast from this position going forward 
        if(Physics.Raycast(this.transform.position + new Vector3(0, 1f, 0), this.transform.forward, out hit)) {
            Debug.Log(hit.transform.name);
            //vfx
            TrailRenderer trail = Instantiate(gunData.bulletTrail, this.transform.position, Quaternion.identity);
            StartCoroutine(calculateVfx(trail, hit));
            target currentTarget = hit.transform.GetComponent<target>();
            if(currentTarget!=null) {
                currentTarget.takeDamage(gunData.damage);
            }
        }
    }

    private IEnumerator calculateVfx(TrailRenderer currentTrail, RaycastHit hit) {
        float time = 0;
        Vector3 startPosition = currentTrail.transform.position;

        while (time < 1) {
            currentTrail.transform.position = Vector3.Lerp(startPosition, hit.point, time);
            time += Time.deltaTime/currentTrail.time;
            yield return null;
        }
        currentTrail.transform.position = hit.point;
        //instantiaza impactul aici dupa;
    }
}