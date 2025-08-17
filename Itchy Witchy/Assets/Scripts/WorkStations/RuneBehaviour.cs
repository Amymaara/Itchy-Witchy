using UnityEngine;

public class RuneBehaviour : MonoBehaviour
{
    [Header("Stamps")]
    public GameObject star;
    public GameObject square;
    public GameObject triangle;
    public GameObject canvas;

    [Header("Player Movement")]
    public GameObject cursor;
    public GameObject playerLine;

    [Header("Runes")]
    public RuneInteractables inputProduct;
    public RuneInteractables outputProduct;
    public Material wood;
    public Material bone;
    public Material stone;

    public void OnRuneTableInteract()
    {
        if (inputProduct != null)
        { 
            if (inputProduct.material == RuneInteractables.RuneMaterial.Wood)
            {
                inputProduct.gameObject.GetComponent<Renderer>().material = wood;
            }
            else if (inputProduct.material == RuneInteractables.RuneMaterial.Stone)
            {
                inputProduct.gameObject.GetComponent<Renderer>().material = stone;
            }
            else if (inputProduct.material == RuneInteractables.RuneMaterial.Bone)
            {
                inputProduct.gameObject.GetComponent<Renderer>().material = bone;
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
