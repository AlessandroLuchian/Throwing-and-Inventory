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
            //in loc de new vector de aici va trebui pusa pozitia armei, la fel si pentru trail
            ParticleSystem particleSystem = Instantiate(gunData.muzzleFlash, this.transform.position +  new Vector3(-1, 0, 0) , Quaternion.identity);
            TrailRenderer trail = Instantiate(gunData.bulletTrail, this.transform.position, Quaternion.identity);
            StartCoroutine(calculateVfx_BulletTrail(trail, hit));
            target currentTarget = hit.transform.GetComponent<target>();
            if(currentTarget!=null) {
                currentTarget.takeDamage(gunData.damage);
            }
        }
    }

    //posibile probleme daca inamicul se misca... In loc de Raycast HIT ar merge un vector3 cu pozitia din viitor a celui in care se trage
    private IEnumerator calculateVfx_BulletTrail(TrailRenderer currentTrail, RaycastHit hit) {
        float time = 0;
        Vector3 startPosition = currentTrail.transform.position;
        //godda go fast
        while (time < 1) {
            currentTrail.transform.position = Vector3.Lerp(startPosition, hit.point, time);
            time += Time.deltaTime/currentTrail.time;
            yield return null;
        }
        currentTrail.transform.position = hit.point;
        //instantiaza impactul aici dupa;
        Destroy(currentTrail.gameObject, currentTrail.time);
    }
}