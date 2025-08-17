using UnityEngine;

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
