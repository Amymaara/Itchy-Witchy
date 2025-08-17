using Unity.VisualScripting;
using UnityEngine;

public class RuneWorkstation : MonoBehaviour, IInteractable
{
    public GameObject heldObject;
    public RuneBehaviour runeBehavior;
    public RuneInteractables playerRune;
    

    public void Interact()
    {
        Debug.Log("trying to interact");
        if (heldObject == null)
        {
            Debug.Log("held object is null");
            return;
        }
        if (heldObject.GetComponentsInChildren<RuneInteractables>() != null)
        {
            RuneInteractables[] runeBase = heldObject.GetComponentsInChildren<RuneInteractables>();

            foreach (RuneInteractables rune in runeBase)
            {
                if (rune.finishedProduct == false)
                {
                    playerRune = rune;
                    runeBehavior.OnRuneTableInteract(playerRune);
                    Debug.Log("Interacting with Ruin Table");
                }

            }
        }
        else 
        {
            return;
        }
        
    }
}
