using UnityEngine;

public enum TarotCards
{
    CauseOfDeath,
    Item,
    ReasonWhy
}

[CreateAssetMenu(fileName = "NewCard", menuName = "Tarot/Card")]
public class TarotCardType : ScriptableObject
{
    public string cardName;
    [TextArea] public string description;
    public Sprite cardFront;

    public TarotCards cardType;
    public string itemID;
}
