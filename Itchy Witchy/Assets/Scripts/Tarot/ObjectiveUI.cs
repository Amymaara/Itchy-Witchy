using UnityEngine;
using TMPro;

public class ObjectiveUI : MonoBehaviour
{
    public GameObject objectiveCardUI;
    public TMP_Text objectiveText;

    void Start()
    {
        objectiveCardUI.SetActive(false);
    }

    public void SetObjective(string itemID)
    {
        objectiveText.text = "Objective:" + itemID;
        // Additional logic for updating the UI can be added here
    }

    public void ShowObjectiveCard()
    {
        objectiveCardUI.SetActive(true);

    }
}
