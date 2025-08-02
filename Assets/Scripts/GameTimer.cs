using UnityEngine;

public class GameTimer : MonoBehaviour // Renamed the class here
{
    public float timeRemaining;
    public bool timerIsRunning = false;
    public TextMesh timerText; 
    public bool useMinutesAndSeconds = false;
    public bool displayMilliseconds = false;
    public bool countUp = false;

    // Public property to get the time remaining
    public float GetTimeRemaining()
    {
        return timeRemaining;
    }

    private void Start()
    {
        // Optionally set initial time here.
        if (timeRemaining <= 0)
        {
            timeRemaining = 0;
        }
        timerIsRunning = true;

        if (timerText == null)
        {
            timerText = GetComponent<TextMesh>();
            if (timerText == null)
            {
                Debug.LogError("GameTimer: No TextMesh component found on the same GameObject! Please assign a TextMesh component in the Inspector, or add one to this GameObject.");
            }
        }
    }

    private void Update()
    {
        if (timerIsRunning)
        {
            if (countUp)
            {
                timeRemaining += Time.deltaTime;
                DisplayTime();
            }
            else if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime();
            }
            else
            {
                Debug.Log("Time has run out!");
                timeRemaining = 0;
                timerIsRunning = false;
                DisplayTime();
            }
        }
    }

    private void DisplayTime()
    {
        if (timerText != null)
        {
            if (useMinutesAndSeconds)
            {
                int minutes = Mathf.FloorToInt(timeRemaining / 60);
                int seconds = Mathf.FloorToInt(timeRemaining % 60);
                string formattedTime = string.Format("{0:00}:{1:00}", minutes, seconds);
                if (displayMilliseconds)
                {
                    int milliseconds = Mathf.FloorToInt((timeRemaining * 1000) % 1000);
                    formattedTime += string.Format(".{0:000}", milliseconds);
                }
                timerText.text = formattedTime;
            }
            else
            {
                if (displayMilliseconds)
                {
                    int milliseconds = Mathf.FloorToInt((timeRemaining * 1000) % 1000);
                    timerText.text = string.Format("{0:0}.{1:000}", Mathf.FloorToInt(timeRemaining), milliseconds);
                }
                else
                {
                    timerText.text = Mathf.Round(timeRemaining).ToString();
                }
            }
        }
        else
        {
            Debug.LogWarning("GameTimer TextMesh element is null in DisplayTime()!");
        }
    }

    public void StartTimer()
    {
        timerIsRunning = true;
    }

    public void StopTimer()
    {
        timerIsRunning = false;
    }

    public void AddTime(float timeToAdd)
    {
        timeRemaining += timeToAdd;
    }

    public void ResetTimer(float newTime)
    {
        timeRemaining = newTime;
        timerIsRunning = true;
        DisplayTime();
    }
}