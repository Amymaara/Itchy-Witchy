using UnityEngine;

[CreateAssetMenu(menuName = "DaySequence")]
public class DaySequence : ScriptableObject
{
    [System.Serializable]
    public struct Entry
    {
        public Customer customerPrefab;
        public ItemSO order;
    }

    public string dayName = "Day X";
    public Entry[] queue;
}
