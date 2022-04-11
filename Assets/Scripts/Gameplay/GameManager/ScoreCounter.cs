using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    [SerializeField] GameObject scoreDisplay;

    int _score = 0;

    private void Update()
    {
        scoreDisplay.GetComponent<NumberDisplayDefenition>()._numericValue = _score.ToString();
    }

    public int GetScoreCount()
    {
        return _score;
    }

    public void AddScore(int amount)
    {
        _score += amount;
    }

    public void RemoveScore(int amount)
    {
        _score -= amount;
    }
}
