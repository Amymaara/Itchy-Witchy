using UnityEngine;

[CreateAssetMenu(menuName = "DaySequence")]
public class DaySequence : ScriptableObject
{
    [System.Serializable]
    public struct Entry //SO queue for how customers appear in order
    {
        public Customer customerPrefab;
        public bool orderFromTarot; // daily play usage
        public ItemSO fixedOrder; // tutorial usage
    }

    public string dayName = "Day X";
    public Entry[] queue;
}
