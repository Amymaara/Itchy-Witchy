using UnityEngine;
using UnityEngine.UI;

public class PotionFillManager : MonoBehaviour
{
    [Header("UI Setup")]
    public Transform fillContainer;       // Vertical Layout Group parent
    public GameObject fillSectionPrefab;  // simple UI Image prefab
    public float maxFillHeight = 300f;    // maximum total height of fill

    [Header("Current Ingredient")]
    public InteractableObject holding;
    public PotionInteractables ingredient;
    public FPController fpcontroller;

    private float currentHeight = 0f;
    private GameObject currentSection;
    private RectTransform currentRect;
    private bool filling = false;

    
    public void StartSection()
    {
        holding = fpcontroller.holdObject;
        if (holding == null) return;

        else 
        {
            PotionInteractables temp = holding.GetComponent<PotionInteractables>();
            if (temp != null) 
            {
                ingredient = temp;
            }
        }
        if (ingredient == null) return;
    
        if (currentHeight >= maxFillHeight) return;

        currentSection = Instantiate(fillSectionPrefab, fillContainer);
        currentRect = currentSection.GetComponent<RectTransform>();
        currentRect.sizeDelta = new Vector2(currentRect.sizeDelta.x, 0f);

        
        Image img = currentSection.GetComponent<Image>();
        img.color = ingredient.fillColour;

        filling = true;
    }

   
    public void GrowSection()
    {
        if (!filling || currentRect == null) return;


        float growth = 250f * Time.deltaTime;
        float availableSpace = maxFillHeight - currentHeight;
        float newHeight = Mathf.Min(currentRect.sizeDelta.y + growth, availableSpace);

        currentRect.sizeDelta = new Vector2(currentRect.sizeDelta.x, newHeight);


        if (currentHeight + growth > maxFillHeight)
        {
            newHeight = maxFillHeight - currentHeight;
            filling = false;
        }

        currentRect.sizeDelta = new Vector2(currentRect.sizeDelta.x, newHeight);
       
    }

   
    public void StopSection()
    {
        if (!filling || currentRect == null) return;

        currentHeight += currentRect.sizeDelta.y;
        filling = false;
        currentSection = null;
        currentRect = null;
    }

  


    /*
      public void ResetPie()
      {
          foreach (Transform child in fillContainer)
          {
              Destroy(child.gameObject);
          }
          currentHeight = 0f;
      }
    */
}
