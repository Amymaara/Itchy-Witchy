using UnityEngine;

// adapted from this video. I didnt include any of the animations they did since this is just a prototype.
//Title: How to Switch Between Cameras in Unity | Camera Switch
//Author: BudGames
//Date Created: 28 Dec 2022
//Date accessed: 17 August 2025
//Code version: 1
//Availability: https://www.youtube.com/watch?v=_yR9FL4LXGE&t=1s

public class CameraManager : MonoBehaviour
{
    public Camera playerCamera;
    public Camera runeCamera;

    private Camera activeCamera;

    void Start()
    {
        SetActiveCamera(playerCamera); 
    }

    public void SetActiveCamera(Camera cam)
    {
        if (activeCamera != null) activeCamera.enabled = false;

        cam.enabled = true;
        activeCamera = cam;
    }

    public void SwitchToRuneCam()
    {
        SetActiveCamera(runeCamera);
    }

    public void SwitchToPlayerCam()
    {
        SetActiveCamera(playerCamera);
    }
}
