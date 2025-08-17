using UnityEngine;
using UnityEngine.EventSystems;

public class RuneBehaviour : MonoBehaviour
{
    [Header("Stamps")]
    public GameObject star;
    public GameObject square;
    public GameObject triangle;
    public GameObject canvas;
    public GameObject firstButton;

    [Header("Player Movement")]
    public GameObject cursor;
    public GameObject playerLine;
    public InputManager inputManager;

    [Header("Runes")]
    public RuneInteractables inputProduct;
    public RuneInteractables outputProduct;
    public Material wood;
    public Material bone;
    public Material stone;

    

   
    public void OnRuneTableInteract(RuneInteractables input)
    {
        inputManager.SwitchToRuneDrawing();
        EventSystem.current.SetSelectedGameObject(firstButton);
        canvas.SetActive(true);
        if (input != null)
        { 
            inputProduct = input;
            if (input.material == RuneInteractables.RuneMaterial.Wood)
            {
                input.gameObject.GetComponent<Renderer>().material = wood;
            }
            else if (input.material == RuneInteractables.RuneMaterial.Stone)
            {
                input.gameObject.GetComponent<Renderer>().material = stone;
            }
            else if (input.material == RuneInteractables.RuneMaterial.Bone)
            {
                input.gameObject.GetComponent<Renderer>().material = bone;
            }
        }
    }

    public void OnStarButton()
    {
        star.SetActive(true);
        cursor.SetActive(true);
        playerLine.SetActive(true);
        canvas.SetActive(false);
    }

    public void OnSquareButton() 
    { 
        square.SetActive(true);
        cursor.SetActive(true);
        playerLine.SetActive(true);
        canvas.SetActive(false);
    }

    public void OnTriangleButton() 
    { 
        triangle.SetActive(true);
        cursor.SetActive(true);
        playerLine.SetActive(true);
        canvas.SetActive(false);
    }


}
