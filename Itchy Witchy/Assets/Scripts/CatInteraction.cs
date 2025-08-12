using UnityEngine;

public class CatInteraction : MonoBehaviour
{
    public string CatName = "Belladonna";
    private bool isPlayerNear = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
            Debug.Log($"press E to talk to {CatName}");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
            Debug.Log($"You have left the interaction area with {CatName}");
        }
    }
    void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.E))
        {
        Interact();
        }
    }
    // Change the Interact method from private to public
    public void Interact()
    {
        Debug.Log($"You are now interacting with {CatName}");
        // can open dialogue ui here
    }
}
