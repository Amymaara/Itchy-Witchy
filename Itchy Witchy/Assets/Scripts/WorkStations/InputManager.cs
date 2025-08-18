using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public GameObject player;
    public GameObject cursor;
    public CameraManager manager;
    public GameObject RuneMinigame;
    public GameObject popUpCanvas;
    public GameObject ToolTipsCanvas;

    private void Start()
    {
        SwitchToUI();
        ToolTipsCanvas.SetActive(true);

    }

    public void SwitchToGameplay()
    {
        RuneMinigame.SetActive(false);
        cursor.SetActive(false);
        player.SetActive(true);
        player.GetComponent<PlayerInput>().SwitchCurrentActionMap("Player");
        manager.SwitchToPlayerCam();
    }

    public void SwitchToUI()
    {
        player.SetActive(true);
        player.GetComponent<PlayerInput>().SwitchCurrentActionMap("UI");
    }

    public void SwitchToRuneDrawing()
    {
        RuneMinigame.SetActive(true);
        player.SetActive(false);
        cursor.SetActive(true);
        manager.SwitchToRuneCam();
    }

    public void OnMenuExit(InputAction.CallbackContext context)
    {
        SwitchToGameplay();
        foreach (Canvas canvas in popUpCanvas.GetComponentsInChildren<Canvas>())
        {
            canvas.gameObject.SetActive(false);
        }
        
    }
}
