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
    public RuneDraw drawing;

    [Header("Runes")]
    public RuneInteractables inputProduct;
    public GameObject runeOnTable;
    public RuneInteractables outputProduct;
    public RuneWorkstation workstation;
    public Material wood;
    public Material bone;
    public Material stone;

    public Material Star;
    public Material Triangle;
    public Material Square;
   

   
    public void OnRuneTableInteract(RuneInteractables input)
    {
        
        if (input != null)
        { 
            inputProduct = input;
            if (input.material == RuneInteractables.RuneMaterial.Wood)
            {
                runeOnTable.gameObject.GetComponent<Renderer>().material = wood;
            }
            else if (input.material == RuneInteractables.RuneMaterial.Stone)
            {
                runeOnTable.gameObject.GetComponent<Renderer>().material = stone;
            }
            else if (input.material == RuneInteractables.RuneMaterial.Bone)
            {
                runeOnTable.gameObject.GetComponent<Renderer>().material = bone;
            }
        }

        drawing.canDraw = false;
        inputManager.SwitchToRuneDrawing();
        EventSystem.current.SetSelectedGameObject(firstButton);
        canvas.SetActive(true);
    }

    public void OnStarButton()
    {
        star.SetActive(true);
        cursor.SetActive(true);
        playerLine.SetActive(true);
        canvas.SetActive(false);
        workstation.playerRune.stamp = RuneInteractables.Stamp.Star;
        workstation.playerRune.secondMesh.SetActive(true);
        workstation.playerRune.secondMesh.GetComponent<Renderer>().material = Star;
        drawing.canDraw = true;

    }

    public void OnSquareButton() 
    { 
        square.SetActive(true);
        cursor.SetActive(true);
        playerLine.SetActive(true);
        canvas.SetActive(false);
        workstation.playerRune.stamp = RuneInteractables.Stamp.Square;
        workstation.playerRune.secondMesh.SetActive(true);
        workstation.playerRune.secondMesh.GetComponent<Renderer>().material = Square;
        drawing.canDraw = true;
    }

    public void OnTriangleButton() 
    { 
        triangle.SetActive(true);
        cursor.SetActive(true);
        playerLine.SetActive(true);
        canvas.SetActive(false);
        workstation.playerRune.stamp = RuneInteractables.Stamp.Triangle;
        workstation.playerRune.secondMesh.SetActive(true);
        workstation.playerRune.secondMesh.GetComponent<Renderer>().material = Triangle;
        drawing.canDraw = true;
    }


}
