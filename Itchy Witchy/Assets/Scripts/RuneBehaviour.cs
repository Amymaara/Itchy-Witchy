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
