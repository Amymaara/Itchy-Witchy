using UnityEngine;

public class PotionWorkstation : MonoBehaviour, IFillable
{
    public PotionFillManager fillManager;
    public void OnFillStart()
    {
        Debug.Log("Trying To Fill");
        
            fillManager.StartSection();
    }

    public void OnFillStop()

    {
        Debug.Log("Stopping Fill");
        fillManager.StopSection();
    }

    public void Fill()
    {
        fillManager.GrowSection();
        
    }


}
