using UnityEngine;
using UnityEngine.InputSystem;

public class BookShelf : MonoBehaviour, IInteractable
{
    public GameObject bookCanvas;
    public InputManager inputManager;
  


    public void  Interact()
    {
        inputManager.SwitchToUI();
        bookCanvas.SetActive(true);
    }


    public void OnMenuExit(InputAction.CallbackContext context) 
    {
        inputManager.SwitchToGameplay();
        bookCanvas.SetActive(false);
    }

}
