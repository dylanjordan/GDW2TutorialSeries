using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTimer : MonoBehaviour
{
    [SerializeField] GameObject TimerDisplay;

    float currentTime;
    float maxTime = 300f;

    int Timer;

    private void Start()
    {
        currentTime = maxTime;
    }
    private void Update()
    {
        currentTime -= 1 * Time.deltaTime;
        Timer = (int)currentTime;
     
        TimerDisplay.GetComponent<NumberDisplayDefenition>()._numericValue = Timer.ToString();
    }

    public void TimerAddScore()
    {
        float time = 300 - currentTime;
        int score = (int)time * 10;
        FindObjectOfType<ScoreCounter>().AddScore(score);

    }
}
