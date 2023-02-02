using TMPro;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    public static TimeController instance;

    public TMP_Text invokeText;
    public float increment = 0.1f;
    private float currentIncrement;
    private bool timerStarted;
    private float invokeTime;

    private void Awake()
    {
        instance = this;
    }

    public void StartTimer() 
    {
        InvokeRepeating("InvokeTimer", 0, increment);
    }
    void InvokeTimer()
    {
        invokeText.text = invokeTime.ToString("00:00.00");
        invokeTime = invokeTime + increment;

    }

    public void Pause()
    {
        currentIncrement = increment;
        increment= 0;
    }
    public void Unpause() 
    {
        increment = currentIncrement;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Pause();
        }

        if (Input.GetKeyUp(KeyCode.P))
        {
            Unpause();
        }

    }
}
