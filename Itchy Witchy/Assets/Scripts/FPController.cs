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

    [Header("UI Elements")]
    public TextMeshProUGUI pickupText;

    [Header("Audio")]
    public WalkingAudio walkingSound;

    [Header("Interaction")]
    [SerializeField] private float interactRange = 3f;

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
        HandleMovement();
        HandleLook();

        if (heldObject != null)
        {
            heldObject.MoveToHoldPoint(holdPoint.position);
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
                    }
                }
            }
            else
            {
                heldObject.Drop();
                heldObject = null;
            }
        }
    }

    public void OnInteract(InputAction.CallbackContext ctx)
    {
        if (!ctx.performed) return;

        if (dialogueController && dialogueController.gameObject.activeInHierarchy)
        {
            dialogueController.DisplayNextParagraph(dialogueText);
            return;
        }
        
        Ray ray = new Ray(cameraTransform.position, cameraTransform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, interactRange))
        {
            var interactable = hit.collider.GetComponent<IInteractable>();
            if (interactable != null) interactable.Interact();
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
