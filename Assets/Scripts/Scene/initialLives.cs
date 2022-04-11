using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class initialLives : MonoBehaviour
{
    [SerializeField] int _initialLives = 5;

    private void Start()
    {
        PlayerPrefs.SetInt("Lives", _initialLives);
    }
}
