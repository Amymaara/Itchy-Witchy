using UnityEngine;
using UnityEngine.InputSystem;

public class Interaction : MonoBehaviour
{
    private PlayerInput playerInput;
    private Transform trans;
  
    public LayerMask interactLayer;

    //ErenCode


    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        trans = transform;
    }


    private void OnEnable()
    {
        playerInput.actions["Interact"].performed += DoInteract;
    }


    private void OnDisable()
    {
        playerInput.actions["Interact"].performed -= DoInteract;
    }


    private void DoInteract(InputAction.CallbackContext context)
    {
        
        if (!Physics.Raycast(trans.position + (Vector3.up * 0.3f) + (trans.forward * 0.2f), 
            trans.forward, out var hit, 1.5f, interactLayer))
        {
            return;
        }
        Debug.Log("Interacting");
    }

}
