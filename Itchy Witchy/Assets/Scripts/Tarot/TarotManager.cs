using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System.Collections;

public class TarotManager : MonoBehaviour
{
    public GameObject tarotCanvas;
    public ObjectiveUI objectiveUI;
    public Button[] cardButtons;
    
    public Image[] cardImages;
    public TMP_Text[] cardDescriptions;
    public Button continueButton;
    public Sprite backOfCardSprite;
    public PlayerInput playerInput;

    private TarotCards[] spread = new TarotCards[3];
    private bool[] revealed = new bool[3];
    private string itemToMake;

    public TarotCards[] causeOfDeathCards;
    public TarotCards[] itemCards;
    public TarotCards[] reasonWhyCards;

    private bool isSpreadActive = false;
    public void OpenTarotSpread()
    {
        if(isSpreadActive) return;
            isSpreadActive = true;
            tarotCanvas.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        StartSpread();
    }

    private void StartSpread()
    {
        //Debug.Log("StartSpread called");

        itemToMake = "";

        spread[0] = causeOfDeathCards[Random.Range(0, causeOfDeathCards.Length)];
        spread[1] = itemCards[Random.Range(0, itemCards.Length)];
        spread[2] = reasonWhyCards[Random.Range(0, reasonWhyCards.Length)];

        continueButton.gameObject.SetActive(false);

        for (int i = 0; i < 3; i++)
        {
            revealed[i] = false;
            cardImages[i].sprite = backOfCardSprite;
            cardDescriptions[i].text = "";

            int buttonIndex = i;
            cardButtons[i].onClick.RemoveAllListeners();
            cardButtons[i].onClick.AddListener(() => RevealCardAnimated(buttonIndex));
        }
    }
    public void RevealCard(int index)
    {
        if (revealed[index]) return;
            revealed[index] = true;

        //Debug.Log($"Flipped card {index}: image={cardImages[index].name}, text={cardDescriptions[index].name}");

        cardImages[index].sprite = spread[index].cardFront;
        cardDescriptions[index].text = spread[index].heading + "\n\n" + spread[index].description;

        if (spread[index].cardType == TarotCardType.Item)
        {
            itemToMake = spread[index].itemID;
        }

        if (revealed[0] && revealed[1] && revealed[2])
        {
            continueButton.gameObject.SetActive(true);
        }
    }
    private void CloseSpread()
    {
         tarotCanvas.SetActive(false);

         Cursor.lockState = CursorLockMode.Locked;
         Cursor.visible = false;

         playerInput.SwitchCurrentActionMap("Player");

         Debug.Log($"Objective item to make: {itemToMake}");
         objectiveUI.SetObjective(itemToMake);
         objectiveUI.ShowObjectiveCard();

         isSpreadActive = false;
    }

    public void OnContinueButton()
    {
        CloseSpread();
    }

    public float flipDuration = 0.5f;

    private void RevealCardAnimated(int index)
    {
        if (revealed[index]) return;
            revealed[index] = true;
            StartCoroutine(FlipCard(index));
    }
    private IEnumerator FlipCard(int index)
    {
        float elapsedTime = 0f;
        float halfDuration = flipDuration / 2f;

        while (elapsedTime < halfDuration)
        {
            elapsedTime += Time.deltaTime;
            float yRotation = Mathf.Lerp(0f, 90f, elapsedTime / halfDuration);
            cardImages[index].rectTransform.localRotation = Quaternion.Euler(0f, yRotation, 0f);
            yield return null;
        }
        
        cardImages[index].sprite = spread[index].cardFront;
        cardDescriptions[index].text = spread[index].heading + "\n\n" + spread[index].description;
        revealed[index] = true;
        elapsedTime = 0f;

        while (elapsedTime < halfDuration)
        {
            elapsedTime += Time.deltaTime;
            float yRotation = Mathf.Lerp(90f, 0f, elapsedTime / halfDuration);
            cardImages[index].rectTransform.localRotation = Quaternion.Euler(0f, yRotation, 0f);
            yield return null;
        }

        cardImages[index].rectTransform.localRotation = Quaternion.identity;

        if (spread[index].cardType == TarotCardType.Item)
        {
            itemToMake = spread[index].itemID;
        }

        if (revealed[0] && revealed[1] && revealed[2])
        {
            continueButton.gameObject.SetActive(true);
        }
    }
}
