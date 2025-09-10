using UnityEngine;
using TMPro;

public class ObjectiveUI : MonoBehaviour
{
    public TMP_Text objectiveText;

    public void SetObjective(string itemID)
    {
        objectiveText.text = "Objective:" + itemID;
        // Additional logic for updating the UI can be added here
    }
}
