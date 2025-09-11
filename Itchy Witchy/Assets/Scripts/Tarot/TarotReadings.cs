using UnityEngine;

// can create a reading by grouping the 3 card types together
[CreateAssetMenu(fileName = "NewTarotReadings", menuName = "Tarot/Readings")]
public class TarotReadings : ScriptableObject
{
    public TarotCards causeOfDeathCards;
    public TarotCards itemCards;
    public TarotCards reasonWhyCards;
}
