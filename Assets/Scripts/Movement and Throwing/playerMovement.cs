using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    private Rigidbody rb; 
    [SerializeField] private Camera cursorCamera;
    [SerializeField] private LayerMask layerMaskGround;
    [SerializeField] private LayerMask layerMaskEnemy;
    private Vector3 movementInput;
    private InputHandler inputHandler;
    [SerializeField] private PlayerData playerData;
    
    //--> TODO REFACTOR SCRIPT (but works for now)
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        this.inputHandler = GetComponent<InputHandler>();
    }

    void FixedUpdate()
    {
        gatherInput();
        movePlayerSkewed();
        if(Input.GetMouseButton(1)) {
            //rotatePlayerToMouse();
            lookOntoTarget();
        }
    }

    void gatherInput() {
        movementInput = inputHandler.getMovementInput();
    }

    //look to make this script more undestandable
    //state machine?
    void movePlayerSkewed() {
        if(movementInput != Vector3.zero) {
            var matrix = Matrix4x4.Rotate(Quaternion.Euler(0, 45, 0));

            var skewedInput = matrix.MultiplyPoint3x4(movementInput);

            //if the player is not aiming and is not sprinting
            if(!Input.GetMouseButton(1) && !Input.GetKey(KeyCode.LeftShift)) {
                rb.MovePosition(transform.position + skewedInput.normalized  * playerData.walkModifier * Time.deltaTime);
                //rotate twoards the position that the player is facing
                Quaternion toRotation = Quaternion.LookRotation(skewedInput, Vector3.up);

                transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, playerData.rotationSpeed * Time.deltaTime);
            }
            //if the player is aiming don't rotate because the player model is already rotating twoards mouse
            else if (Input.GetMouseButton(1)) {
                rb.MovePosition(transform.position + skewedInput.normalized  * playerData.slowWalkModifier * Time.deltaTime);
            }
            //if the player is sprinting 
            else {
                rb.MovePosition(transform.position + skewedInput.normalized  * playerData.sprintModifier * Time.deltaTime);
                //rotate twoards the position that the player is facing
                 Quaternion toRotation = Quaternion.LookRotation(skewedInput, Vector3.up);

                transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, playerData.rotationSpeed * Time.deltaTime);
            }
        }
    }

    void rotatePlayerToMouse () {
        Vector3 mouseInput = Input.mousePosition;
        Ray ray = cursorCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, float.MaxValue, layerMaskGround)) {
            //Debug.DrawRay(new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, Camera.main.transform.position.z), new Vector3(hit.point.x, transform.position.y, hit.point.z) - transform.position);
            var hitObject = hit.transform.gameObject.GetComponent<MeshRenderer>();
            Debug.Log(hitObject);
            Quaternion toRotation = Quaternion.LookRotation(new Vector3(hit.point.x, transform.position.y, hit.point.z) - transform.position);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, playerData.aimRoationSpeed * Time.deltaTime);
        }
    }

     private bool lookOntoTarget () {
        Vector3 mouseInput = Input.mousePosition;
        Ray ray = cursorCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, float.MaxValue, layerMaskEnemy)) {
            Debug.Log("am intrat in coliziune cu inamicul");
            var hitObject = hit.transform.gameObject.GetComponent<Collider>();
            Debug.Log(hitObject);
            Quaternion toRotation = Quaternion.LookRotation(hit.collider.bounds.center - new Vector3(0, hit.transform.position.y, 0) + new Vector3(0, this.transform.position.y, 0)- transform.position);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, playerData.aimRoationSpeed * Time.deltaTime);
            return true;
        }
        return false;
    }
}
