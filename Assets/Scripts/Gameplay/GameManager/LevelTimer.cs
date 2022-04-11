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

        if (currentTime <= 0.0f)
        {
            FindObjectOfType<LevelStatus>().SetGameOver(true);
        }
    }

   
}
