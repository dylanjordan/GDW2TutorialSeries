using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCounter : MonoBehaviour
{
    [SerializeField] GameObject coinDisplay;

    int _coinCount = 0;

    private void Update()
    {
        coinDisplay.GetComponent<NumberDisplayDefenition>()._numericValue = _coinCount.ToString();
    }

    public int GetCoinCount()
    {
        return _coinCount;
    }

    public void AddCoin(int amount)
    {
        _coinCount += amount;
    }

    public void RemoveCoin(int amount)
    {
        _coinCount -= amount;
    }
}
