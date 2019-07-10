using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Clock : MonoBehaviour
{
    private Text textClock;

    private int seconds;
    private int minutes;

    private void Awake()
    {
        textClock = GetComponent<Text>();
    }

    private void Start()
    {
        Time.timeScale = 1;
        StartCoroutine(UpdateClock());
    }

    private IEnumerator UpdateClock()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            seconds++;
            if (seconds >= 59)
            {
                seconds = 0;
                minutes++;
            }
            string minute = LeadingZero(minutes);
            string second = LeadingZero(seconds);
            textClock.text = minute + ":" + second;
        }
    }

    private string LeadingZero(int n)
    {
        return n.ToString().PadLeft(2, '0');
    }
}