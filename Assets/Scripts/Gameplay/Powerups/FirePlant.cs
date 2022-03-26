using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePlant : MonoBehaviour
{
    public bool _isMoving = false;

    public float _speed = 1.1f;

    // Update is called once per frame
    void Update()
    {
        UpdatePowerup();
    }
    public virtual void UpdatePowerup()
    {
        if (_isMoving)
        {
            transform.position += Vector3.left * Time.deltaTime * _speed;
        }
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        _isMoving = true;
    }
}
