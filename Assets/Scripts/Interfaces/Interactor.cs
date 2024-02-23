using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interactor : MonoBehaviour
{
    public Transform InteractionPoint;
    public LayerMask InteractionLayer;
    public playerMovement Movement;
    public PlayerInventoryHolder OpenChestAndBackpack;
    public float InteractionPointRadius = 1f;
    public bool isInteracting { get; private set; }

    private void Start() {
        Movement = GetComponent<playerMovement>();
    }
    private void Update(){
        var colliders = Physics.OverlapSphere(InteractionPoint.position, InteractionPointRadius, InteractionLayer);
        if(Keyboard.current.eKey.wasPressedThisFrame){
            for(int i=0; i < colliders.Length; i++){
                var interactable = colliders[i].GetComponent<IInteractable>();

                if(interactable != null){
                    StartInteraction(interactable);
                    Movement.enabled = false;
                }
            }
        }
        if(Keyboard.current.escapeKey.wasPressedThisFrame)
            Movement.enabled = true;
    }

    void StartInteraction(IInteractable interactable){
        interactable.Interact(this, out bool interactSuccessful);
    }

    void EndInteraction(){
            isInteracting = false;
    }
}
