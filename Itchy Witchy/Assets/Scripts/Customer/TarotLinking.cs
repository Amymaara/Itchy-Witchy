using UnityEngine;
using System.Collections;
using System;

public class TarotLinking : MonoBehaviour
{
    public static TarotLinking instance {  get; private set; }
    private void Awake() => instance = this;

    public event Action<ItemSO> onTarotOrderChosen;

    public void SetTarotResult(ItemSO item) // links to the Tarot card result
    {
        onTarotOrderChosen?.Invoke(item);  
    }
}
