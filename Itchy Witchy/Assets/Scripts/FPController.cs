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
    public float pickupRange = 3f;
    public Transform holdPoint;
    private PickUpObject heldObject;

    [Header("UI Elements")]
    public TextMeshProUGUI pickupText;

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

        Ray ray = new Ray(cameraTransform.position, cameraTransform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, pickupRange))
        {
            PickUpObject pickUpObject = hit.collider.GetComponent<PickUpObject>();
            if (pickUpObject != null)
            {
                pickupText.text = "Press E to pick up " + pickUpObject.gameObject.name;
                pickupText.gameObject.SetActive(true);
            }
            else
            {
                pickupText.gameObject.SetActive(false);
            }
        }
        else
        {
            pickupText.gameObject.SetActive(false);
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
    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed && controller.isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
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
                    PickUpObject pickUpObject = hit.collider.GetComponent<PickUpObject>();
                    if (pickUpObject != null)
                    {
                        heldObject = pickUpObject;
                        heldObject.PickUp(holdPoint);
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
    public void HandleMovement()
    {
        Vector3 move = transform.right * moveInput.x + transform.forward * moveInput.y;
        controller.Move(move * moveSpeed * Time.deltaTime);

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
