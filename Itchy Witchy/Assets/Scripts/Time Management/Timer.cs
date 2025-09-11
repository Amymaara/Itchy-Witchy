using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] private Image timerBar;

    public int timerDuration;
    private int remainingDuration;

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
