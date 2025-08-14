using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

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

    [Header("Interact Settings")]
    public float interactRange = 3f;
    public Interactable catInteraction;

    [Header("UI Elements")]
    public TextMeshProUGUI pickupText;

    [Header("Audio")]
    public AudioManager audioManager;

    private CharacterController controller;
    private Vector2 moveInput;
    private Vector2 lookInput;
    private Vector3 velocity;
    private float verticalRotation = 0f;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;
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

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Ray ray = new Ray(cameraTransform.position, cameraTransform.forward);
            if (Physics.Raycast(ray, out RaycastHit hit, interactRange))
            {
                Interactable interactable = hit.collider.GetComponent<Interactable>();
                if (interactable != null)
                {
                    interactable.Interact();
                }
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
            audioManager.HandleFootsteps();

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
