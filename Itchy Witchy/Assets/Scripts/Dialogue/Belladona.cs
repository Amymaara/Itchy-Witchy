using UnityEngine;

public class Belladona : NPC, ITalkable
{
    [Header("Dialogue")]
    //serves as our introduction dialogure
    [SerializeField] private DialogueController dialogueController;

    [Header("Outcome Dialogue")]
    [SerializeField] private DialogueText introDialogueText;
    [SerializeField] private DialogueText incompleteDialogue;
    [SerializeField] private DialogueText successDialogue;
    [SerializeField] private DialogueText wrongMaterialsDialogue;
    [SerializeField] private DialogueText lowAccuraryDialogue;

    [Header("State")]
    [SerializeField] private bool introShown = false;
    [SerializeField] RuneOutcome currentOutcome = RuneOutcome.Incomplete;

    //tracks which scriptable object we're using
    private DialogueText lastDialogue;

   public override void Interact()
    {
        if (dialogueController == null)
        {
            Debug.Log(" Belladona: DialogueController not assigned");
            return;
        }

        // if panel open keeps advancing same convo
        if (dialogueController.gameObject.activeSelf && lastDialogue != null)
        {
            Talk(lastDialogue);
            return;
        }

        //otherwise select dialogue from outcome
        DialogueText toPlay = null;

        if (!introShown && introDialogueText != null)
        {
            introShown = true;
            toPlay = introDialogueText;
        }

        else
        {
            toPlay = ChooseDialogueOutcome(currentOutcome);

        }

        lastDialogue = toPlay;

        if (toPlay != null)
        {
            Talk(toPlay);
        }
    }

    public void Talk(DialogueText dialogueText)
    {
        //start conversation
        dialogueController.DisplayNextParagraph(dialogueText);
    }

    public void SetOutcome(RuneOutcome outcome)
    {
        Debug.Log("State = " + outcome);
        currentOutcome = outcome;
    }

    // based on the rune state it returns a different dialogue
    private DialogueText ChooseDialogueOutcome(RuneOutcome outcome)
    {
        switch (outcome)
        {
            case RuneOutcome.Success: return successDialogue;
            case RuneOutcome.WrongMaterials: return wrongMaterialsDialogue;
            case RuneOutcome.LowAccuracy: return lowAccuraryDialogue;
            default: return incompleteDialogue;
        }
    }
}
