using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemThrowing : MonoBehaviour
{
    [Header("References")]
    public Transform attackPoint;
    public GameObject objectToThrow;
    public Transform throwMarker;

    [Header("Settings")]
    public int totalThrows;
    public float throwCooldown;

    public Collider planeCollider;

    public float offset;

    [Header("Throwing")]
    public KeyCode throwKey = KeyCode.Mouse1;
    public float throwForce;
    public float throwUpwardForce;

    bool readyToThrow;

    private void Start()
    {
        readyToThrow = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(throwKey) && readyToThrow && totalThrows > 0)
        {
            Throw();
        }
    }

    private void Throw()
    {
        readyToThrow = false;
        Vector3 attackPointOffset = new Vector3(attackPoint.position.x,attackPoint.position.y + offset,attackPoint.position.z);
        // instantiate object to throw
        GameObject projectile = Instantiate(objectToThrow, attackPointOffset, Quaternion.identity); // No rotation needed in 3D

        // get rigidbody component
        Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();

        // calculate direction (adjust for the fixed camera perspective)
        Vector3 forceDirection = throwToMousePosition()+Vector3.forward; // Use marker's forward direction

        // add force (including some upward force for 3D)
        Vector3 forceToAdd = forceDirection * throwForce + transform.up * throwUpwardForce;

        projectileRb.AddForce(forceToAdd, ForceMode.Impulse);

        totalThrows--;

        // implement throwCooldown
        StartCoroutine(ResetThrow());
    }

    Vector3 throwToMousePosition(){
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit)) {
            if(hit.collider == planeCollider) {
                return new Vector3(hit.point.x,transform.position.y,hit.point.z)-transform.position.normalized;
            }        
        }
        return new Vector3(0,0,0);
    }

    private IEnumerator ResetThrow()
    {
        yield return new WaitForSeconds(throwCooldown);
        readyToThrow = true;
    }
}


