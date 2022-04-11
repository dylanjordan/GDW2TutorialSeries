using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lives : MonoBehaviour
{
    [SerializeField] int _lives = 5;
    [SerializeField] GameObject livesDisplay;

    int _currentLives;


    private void Update()
    {
        _currentLives = PlayerPrefs.GetInt("Lives");

        if (_currentLives < 1)
        {
            GetComponent<LevelStatus>().SetGameOver(true);
        }

        livesDisplay.GetComponent<NumberDisplayDefenition>()._numericValue = _currentLives.ToString();
    }

    public void LoseLife()
    {
        _currentLives--;
        PlayerPrefs.SetInt("Lives", _currentLives);
    }

    public int GetCurrentLives()
    {
        return _currentLives;
    }
}
