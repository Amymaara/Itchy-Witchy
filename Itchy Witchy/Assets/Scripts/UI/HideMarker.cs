using UnityEngine;

public class HideMarker : MonoBehaviour
{
    
    [SerializeField] private DialogueController dialogueController;

    public GameObject arrowMarker;
    public GameObject dialogueBox;

    // checks to see if the dialogue controller & dialogue box is active is scene
    public bool IsDialogueOpen => dialogueController && dialogueController.gameObject.activeInHierarchy;
    public void showArrowMarker()
    {
        arrowMarker.SetActive(true);
    }

    public void hideArrowMarker()
    {
        arrowMarker.SetActive(false);
    }
}
