using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System.Collections;

public class TarotManager : MonoBehaviour
{
    [Header("UI References")]
    public GameObject tarotCanvas;
    public ObjectiveUI objectiveUI;
    public Button[] cardButtons;
    public Image[] cardImages;
    public TMP_Text[] cardDescriptions;
    public Button continueButton;
    public Sprite backOfCardSprite;

    [Header("Player Input")]
    public PlayerInput playerInput;

    [Header("Tarot Data")]
    public TarotReadings[] tarotReadings;   //possible tarot readings
    public TarotCards[] causeOfDeathCards;
    public TarotCards[] itemCards;
    public TarotCards[] reasonWhyCards;

    [Header("Tarot Animation")]
    public float flipDuration = 0.5f;

    private TarotCards[] spread = new TarotCards[3];
    private bool[] revealed = new bool[3];
    private string itemToMake;  // item the player must make
    private bool isSpreadActive = false;    // prevent multiple readings at same time
    private bool orderInProgress = false;
    private TarotReadings currentReading;

    // Public Methods

    // Starts Tarot reading as chosen by customer system
    public void OpenTarotSpread(TarotReadings chosenReading)
    {
        if(isSpreadActive || orderInProgress) return;
            isSpreadActive = true;
            tarotCanvas.SetActive(true);

        // unlock cursor to interact on screen
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        StartSpread(chosenReading); // Setup spread 
    }
    public void CompleteOrder() // called on order completion
    {
        orderInProgress = false;
        objectiveUI.HideObjectiveCard();    // hides ordertab if there is no active order
    }
    public void OnContinueButton()  // closes tarotUI once all cards are flipped
    {
        CloseSpread();
    }

    // Private Methods

    // Ensures card spread has chosen reading
    // Assigns sprites, descriptions etc. to cards
    private void StartSpread(TarotReadings chosenReading)
    {
        //Debug.Log("StartSpread called");

        itemToMake = "";
        currentReading = chosenReading; // store currently selected reading

        // assign cards to spread
        spread[0] = currentReading.causeOfDeathCards;
        spread[1] = currentReading.itemCards;
        spread[2] = currentReading.reasonWhyCards;

        continueButton.gameObject.SetActive(false);

        // initialize cards
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

    // Closes TarotUI
    private void CloseSpread()
    {
         tarotCanvas.SetActive(false);

         // hides cursor
         Cursor.lockState = CursorLockMode.Locked;
         Cursor.visible = false;

         playerInput.SwitchCurrentActionMap("Player");  // switch back to Player controls

         // update orderUI
         objectiveUI.SetObjective(itemToMake);
         objectiveUI.ShowObjectiveCard();

         orderInProgress = true;
         isSpreadActive = false;
    }
    
    // Card flip animation
    private void RevealCardAnimated(int index)
    {
        if (revealed[index]) return;
            revealed[index] = true;
            StartCoroutine(FlipCard(index));
    }

    // Coroutine that animaates card
    private IEnumerator FlipCard(int index)
    {
        float elapsedTime = 0f;
        float halfDuration = flipDuration / 2f;

        // rotate 0 -> 90 degrees (first half of flip)
        while (elapsedTime < halfDuration)
        {
            elapsedTime += Time.deltaTime;
            float yRotation = Mathf.Lerp(0f, 90f, elapsedTime / halfDuration);
            cardImages[index].rectTransform.localRotation = Quaternion.Euler(0f, yRotation, 0f);
            yield return null;
        }
        
        // change sprite to front of card & show descriptions
        cardImages[index].sprite = spread[index].cardFront;
        cardDescriptions[index].text = spread[index].heading + "\n\n" + spread[index].description;

        elapsedTime = 0f;

        // rotate 90 -> 0 degrees (second half of flip)
        while (elapsedTime < halfDuration)
        {
            elapsedTime += Time.deltaTime;
            float yRotation = Mathf.Lerp(90f, 0f, elapsedTime / halfDuration);
            cardImages[index].rectTransform.localRotation = Quaternion.Euler(0f, yRotation, 0f);
            yield return null;
        }

        // resets rotation to default
        cardImages[index].rectTransform.localRotation = Quaternion.identity;

        // store item for orderUI if card is item card
        if (spread[index].cardType == TarotCardType.Item)
        {
            itemToMake = spread[index].itemID;
        }

        // show continue button if all cards flipped
        if (revealed[0] && revealed[1] && revealed[2])
        {
            continueButton.gameObject.SetActive(true);
        }
    }
}
