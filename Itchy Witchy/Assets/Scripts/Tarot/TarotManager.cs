using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class TarotManager : MonoBehaviour
{
    public GameObject tarotCanvas;
    public ObjectiveUI objectiveUI;
    public Button[] cardButtons;
    public Image[] cardImages;
    public TMP_Text[] cardDescriptions;
    public PlayerInput PlayerInput;

    private TarotCardType[] spread = new TarotCardType[3];
    private bool[] revealed = new bool[3];

    public TarotCardType[] causeOfDeathCards;
    public TarotCardType[] itemCards;
    public TarotCardType[] reasonWhyCards;

    public void OpenTarotSpread()
    {
        tarotCanvas.SetActive(true);
        StartSpread();
    }

    void StartSpread()
    {
        spread[0] = causeOfDeathCards[Random.Range(0, causeOfDeathCards.Length)];
        spread[1] = itemCards[Random.Range(0, itemCards.Length)];
        spread[2] = reasonWhyCards[Random.Range(0, reasonWhyCards.Length)];

        for (int i = 0; i < 3; i++)
        {
            revealed[i] = false;
            cardDescriptions[i].text = "???";
        }
    }
    public void RevealCard(int index)
    {
        if (revealed[index]) return;
        revealed[index] = true;

        cardImages[index].sprite = spread[index].cardFront;
        cardDescriptions[index].text = spread[index].description;

        if (spread[index].cardType == TarotCards.Item)
        {
            objectiveUI.SetObjective(spread[index].itemID);
        }

        if (revealed[0] && revealed[1] && revealed[2])
        {
            tarotCanvas.SetActive(false);

            PlayerInput.SwitchCurrentActionMap("Player");
        }
    }
}
