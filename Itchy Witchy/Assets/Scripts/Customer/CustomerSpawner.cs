using System.Collections;
using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{
    [Header("Customer")]
    public DaySequence daySequence; //hold array of customers and what they want 
    public Transform spawnPoint; // sets spawn position for customers
    public float spawnDelay = 0.5f; // time before customer spawns in


    private Customer currentCustomer;
    private int index = -1; // where you are in the queue, set to -1 so when SpawnNext() called goes to 0 

    private void Start()
    {
        SpawnNext(); //immediately spawns customer for now (might change later)
    }

    void SpawnNext()
    {
        index++; //move next slot if queue
        if (daySequence == null || index >= daySequence.queue.Length)
        {
            Debug.Log("day finished");
            return;
        }

        StartCoroutine(CustomerSpawnRoutine(daySequence.queue[index]));
    }
    IEnumerator CustomerSpawnRoutine(DaySequence.Entry entry) //spawns the next customer
    {
        yield return new WaitForSeconds(spawnDelay);
        currentCustomer = Instantiate(entry.customerPrefab, spawnPoint.position, Quaternion.identity);

        if (entry.orderFromTarot) // handles when an order comes from a tarot result 
        {
            void Handler(ItemSO item)
            {
                TarotLinking.instance.onTarotOrderChosen -= Handler;
                if (currentCustomer) currentCustomer.SetOrder(item); // links to my set order function in customer
            }
            TarotLinking.instance.onTarotOrderChosen += Handler; 
        }

        else
        {
            currentCustomer.SetOrder(entry.fixedOrder); // manually set order (turorial usage)
        }

        currentCustomer.OnServedCorrectly += HandleCustomerServed;
    }

    void HandleCustomerServed() //when customer served correctly goes to next customer and their event (order)
    {
        if (currentCustomer != null)
            currentCustomer.OnServedCorrectly -= HandleCustomerServed;

        currentCustomer = null;
        SpawnNext();
    }
    public void DestroyCustomer() //destroys customer 
    {
        if (currentCustomer != null)
        {
            Destroy(currentCustomer.gameObject, 1f);
            currentCustomer = null;
        }
    }
}
