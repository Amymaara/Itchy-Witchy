using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public GameObject player;
    public GameObject cursor;
    public CameraManager manager;
    public GameObject RuneMinigame;
    public void SwitchToGameplay()
    {
        RuneMinigame.SetActive(false);
        cursor.SetActive(false);
        player.SetActive(true);
        manager.SwitchToPlayerCam();
    }

    public void SwitchToRuneDrawing()
    {
        RuneMinigame.SetActive(true);
        player.SetActive(false);
        cursor.SetActive(true);
        manager.SwitchToRuneCam();
    }
}
