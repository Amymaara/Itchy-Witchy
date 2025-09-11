using UnityEngine;

public enum TarotCardType
{
    CauseOfDeath,
    Item,
    ReasonWhy
}

// can create individual cards
[CreateAssetMenu(fileName = "NewCard", menuName = "Tarot/Card")]
public class TarotCards : ScriptableObject
{
    public string cardName;
    public string heading;
    [TextArea] public string description;
    public Sprite cardFront;

    public TarotCardType cardType;
    public string itemID;
}
