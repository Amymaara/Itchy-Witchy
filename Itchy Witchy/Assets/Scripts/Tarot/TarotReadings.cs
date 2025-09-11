using UnityEngine;

[CreateAssetMenu(fileName = "NewTarotReadings", menuName = "Tarot/Readings")]
public class TarotReadings : ScriptableObject
{
    public TarotCards causeOfDeathCards;
    public TarotCards itemCards;
    public TarotCards reasonWhyCards;
}
