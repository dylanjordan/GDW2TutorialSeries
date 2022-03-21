using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goomba : MonoBehaviour
{
    [SerializeField] float deathTimer = 0.2f;

    bool _isSquashed;
    bool _movingLeft;

    float _flipTimer = 0;
    float _speed = 1.5f;

    void Update()
    {
        if (!_isSquashed)
        {
            Move();
        }

        if (_isSquashed)
        {
            Destroy(gameObject, deathTimer);
        }

        if (_flipTimer <= Time.realtimeSinceStartup)
        {
            transform.Rotate(new Vector3(0, 1, 0), 180);
            _flipTimer = Time.realtimeSinceStartup + 0.25f;
        }
    }

    private void Move()
    {
        if (_movingLeft)
        {
            transform.position += Vector3.left * Time.deltaTime * _speed;
        }
        else
        {
            transform.position += Vector3.right * Time.deltaTime * _speed;
        }
    }

    public bool GetIsSquashed()
    {
        return _isSquashed;
    }

    public void SetIsSquashed(bool squashed)
    {
        _isSquashed = squashed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _movingLeft = !_movingLeft;
    }
}
