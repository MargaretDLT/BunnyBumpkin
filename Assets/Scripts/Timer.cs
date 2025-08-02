using UnityEngine;

public class Timer : MonoBehaviour
{
    public float gameTimer;      // countup timer
    TextMesh timerText;

    // Start is called before the first frame update
    void Start()
    {
        gameTimer = 0.0f;
        timerText = GetComponent<TextMesh>();
        timerText.text = "Timer: ---";
        InvokeRepeating("TimerUpdate", 1.0f, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        // increment gameTimer
        gameTimer += Time.deltaTime;
        // if you use this as scoring - for instance a survival game - multiple Time.deltaTime by a points per second - example 10
    }

    void TimerUpdate()
	{
        timerText.text = string.Format("Timer: {0:0}", gameTimer);
    }
}
