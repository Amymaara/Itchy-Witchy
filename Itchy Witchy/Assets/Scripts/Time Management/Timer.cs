using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Image timerBar;

    [Header("Settings")]
    public int timerDuration;
    public int totalCustomers = 0;
    
    private int remainingDuration;
    private int customersServed;
    private bool timerRunning;
    private bool timerStarted;


    void Start()
    {
        Being(timerDuration);
    }

    void Being(int Second)
    {
        remainingDuration = Second;
        StartCoroutine(UpdateTimer());
    }

   IEnumerator UpdateTimer()
    {
        while (remainingDuration >= 0)
        {
            timerBar.fillAmount = Mathf.InverseLerp(0, timerDuration, remainingDuration);
            remainingDuration--;
            yield return new WaitForSeconds(1f);
        }
        OnEnd();
    }

    void OnEnd()
    {
        
    }
}
