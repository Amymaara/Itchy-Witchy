using UnityEngine;

public class Interactable : MonoBehaviour
{
    [Header("Interactable Settings")]
    public string ObjectName = "Object";
    [TextArea] public string InteractionMessage = "Press E to interact with this object.";

    private bool isPlayerNear = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
            Debug.Log($"press E to interact with {ObjectName}");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
            Debug.Log($"You have left the interaction area with {ObjectName}");
        }
    }
    /*void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }
    }
    // Change the Interact method from private to public

    public void Interact()
    {
        Debug.Log($"You are now interacting with {ObjectName}");
        // can open dialogue ui here
    }
  */
}
