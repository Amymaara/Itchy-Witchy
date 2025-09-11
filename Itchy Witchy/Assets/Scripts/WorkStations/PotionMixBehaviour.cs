using UnityEngine;
using UnityEngine.EventSystems;

public class PotionMixBehaviour : MonoBehaviour
{
    public GameObject Button;
    public void OnEnable()
    {
        EventSystem.current.SetSelectedGameObject(Button);
    }
}
