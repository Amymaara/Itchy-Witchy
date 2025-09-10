using UnityEngine;
using UnityEngine.InputSystem;

public class TarotPileInteractable : MonoBehaviour, IInteractable
{
    public TarotManager tarotManager;
    public PlayerInput PlayerInput;

    public void Interact()
    {
        tarotManager.OpenTarotSpread();

        PlayerInput.SwitchCurrentActionMap("Tarot");
    }
}
