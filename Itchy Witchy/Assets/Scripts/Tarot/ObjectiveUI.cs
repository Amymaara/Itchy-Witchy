using UnityEngine;
using TMPro;

public class ObjectiveUI : MonoBehaviour
{
    public GameObject objectiveCardUI;
    public TMP_Text orderText;
    public TMP_Text itemText;

    void Start()
    {
        objectiveCardUI.SetActive(false);
    }

    public void SetObjective(string itemID)
    {
        if(itemText != null)
            itemText.text = itemID;

        else
            Debug.LogWarning("Order Text is not assigned in the inspector.");
    }

    public void ShowObjectiveCard()
    {
        objectiveCardUI.SetActive(true);
    }
}
