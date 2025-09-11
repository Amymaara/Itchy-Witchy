using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

// Title: First Person Controller Script
// Author: Hayes, A
// Date: 09/08/2025
// Avalability: DIGA2001A Lecture Slides
public class FPController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float gravity = -9.81f;
    public float jumpHeight = 2f;

    [Header("Look Settings")]
    public Transform cameraTransform;
    public float lookSensitivity = 2f;
    public float verticalLookLimit = 80f;

    [Header("Pickup Settings")]
    public float pickupRange = 5f;
    public Transform holdPoint;
    private PickUpObject heldObject;
    public InteractableObject holdObject;

    [Header("UI Elements")]
    public TextMeshProUGUI pickupText;

    [Header("Audio")]
    public WalkingAudio walkingSound;

    [Header("Interaction")]
    [SerializeField] private float interactRange = 3f;
    private IFillable cauldronFill;

    [Header("Dialogue")]
    [SerializeField] private DialogueController dialogueController;
    [SerializeField] private DialogueText dialogueText;

    private CharacterController controller;
    private Vector2 moveInput;
    private Vector2 lookInput;
    private Vector3 velocity;
    private float verticalRotation = 0f;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        if (cauldronFill != null && filling)
        {
            cauldronFill.Fill();
        }

        HandleMovement();
        HandleLook();

        if (heldObject != null)
        {
            heldObject.transform.position = holdPoint.position;
        }
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }
    public void OnLook(InputAction.CallbackContext context)
    {
        lookInput = context.ReadValue<Vector2>();
    }

    public void OnPickup(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        {
            if (heldObject == null)
            {
                Ray ray = new Ray(cameraTransform.position, cameraTransform.forward);
                if (Physics.Raycast(ray, out RaycastHit hit, pickupRange))
                {
                    PickUpObject pickUp = hit.collider.GetComponent<PickUpObject>();
                    if (pickUp != null)
                    {
                        pickUp.PickUp(holdPoint);
                        heldObject = pickUp;
                        InteractableObject intObj = pickUp.gameObject.GetComponent<InteractableObject>();
                        if (intObj != null)
                        {
                            holdObject = intObj;
                        }
                    }
                }
            }
            else
            {
                heldObject.Drop();
                heldObject = null;
                holdObject = null;
            }
        }
    }
    public bool filling = false;
    public void OnInteract(InputAction.CallbackContext ctx)
    {
        if (ctx.phase == InputActionPhase.Started)
        {
            Debug.Log("Press started");

            if (dialogueController && dialogueController.gameObject.activeInHierarchy)
            {
                dialogueController.DisplayNextParagraph(dialogueText);
                return;
            }

            // this is for handling logic of giving customer items 

            Ray ray = new Ray(cameraTransform.position, cameraTransform.forward);
            if (Physics.Raycast(ray, out RaycastHit hit, interactRange))
            {
                var customer = hit.collider.GetComponent<Customer>();
                if (customer)
                {
                    var held = holdPoint.GetComponentInChildren<ServeableItem>();
                    if (held)
                    {
                        bool ok = customer.TryServe(held);
                        if (ok) Destroy(held.gameObject);
                    }
                }

                // try and fill in the cauldron bar thing
                if (hit.collider.TryGetComponent<IFillable>(out var fillable))
                {
                    cauldronFill = fillable;
                    cauldronFill.OnFillStart();
                    filling = true;
                    Debug.Log("Started filling");
                    return;
                }

                // normal interactable items
                if (hit.collider.TryGetComponent<IInteractable>(out var interactable))
                {
                    interactable.Interact();
                }
            }

           
        }
        else if (ctx.phase == InputActionPhase.Canceled)
        {
            if (cauldronFill != null)
            {
                Debug.Log("Cancelling Fill");
                cauldronFill.OnFillStop();
                cauldronFill = null;
                filling = false;
            }
        }
    }

      

    


    public void HandleMovement()
    {
        Vector3 move = (transform.right * moveInput.x + transform.forward * moveInput.y).normalized; // normalize movement vector
       
        controller.Move(move * moveSpeed * Time.deltaTime);
        //Debug.Log(move);

        if (moveInput != Vector2.zero)
        {
           // Debug.Log("Moving");
            //audioManager.HandleFootsteps();

        }
        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
    public void HandleLook()
    {
        float mouseX = lookInput.x * lookSensitivity;
        float mouseY = lookInput.y * lookSensitivity;

        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, -verticalLookLimit, verticalLookLimit);

        cameraTransform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }
}
