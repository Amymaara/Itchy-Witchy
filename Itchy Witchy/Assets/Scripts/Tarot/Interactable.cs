using UnityEngine;
using UnityEngine.InputSystem;

public class Interactable : MonoBehaviour
{
    public float interactRange = 3f;
    public LayerMask interactableMask;

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        Camera mainCamera = Camera.main;
        if (mainCamera == null) return;
        Ray ray = new Ray(mainCamera.transform.position, mainCamera.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, interactRange, interactableMask))
        {
            IInteractable interactable = hit.collider.GetComponent<IInteractable>();
            if (interactable != null)
            {
                interactable.Interact();
            }
        }
    }
}
