using System;
using UnityEngine;

public class Customer : MonoBehaviour
{
    public ItemSO requiredItem;
    public event Action OnServedCorrectly;

    public void SetOrder(ItemSO item)
    {
        requiredItem = item; 
    }

    public bool TryServe(ServeableItem served)
    {
        if (served && served.item == requiredItem) //checks if the handed in item is the required item
        {
            Debug.Log("Correct item given");

            OnServedCorrectly?.Invoke();

            Destroy(gameObject, 0.25f);
            return true;
        }

        else
        {
            Debug.Log("Wrong item"); // if item mismatch doesnt accept 
            return false;
        }
    }
}
