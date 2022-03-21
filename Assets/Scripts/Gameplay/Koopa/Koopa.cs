using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Koopa : MonoBehaviour
{
    [SerializeField] float _kickForce = 2.5f;
    [SerializeField] float _speed = 1.5f;

    bool _isSquashed;
    bool _isKicked;
    bool _isMoving = true;
    bool _movingLeft;

    void Update()
    {
        if (!_isSquashed)
        {
            Move();
        }
    }

    void Move()
    {
        if (_movingLeft)
        {
            transform.position += Vector3.left * Time.deltaTime * _speed;
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            transform.position += Vector3.right * Time.deltaTime * _speed;
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }

        _isMoving = true;
    }

    public void ApplyKickForce(Vector2 direction)
    {
        GetComponent<Rigidbody2D>().AddForce(direction * _kickForce, ForceMode2D.Impulse);
        _isKicked = true;
        _isMoving = true;
    }

    public bool GetIsSquashed()
    {
        return _isSquashed;
    }

    public void SetIsSquashed(bool squashed)
    {
        _isSquashed = squashed;
    }

    public bool GetIsKicked()
    {
        return _isKicked;
    }

    public void SetIsKicked(bool kicked)
    {
        _isKicked = kicked;
    }

    public bool GetIsMoving()
    {
        return _isMoving;
    }

    public void SetIsMoving(bool moving)
    {
        _isMoving = moving;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!_isSquashed)
        {
            _movingLeft = !_movingLeft;
        }

        if (_isKicked)
        {
            if (collision.contacts[0].normal.x > 0)
            {
                ApplyKickForce(new Vector2(1, 0));
            }
            if (collision.contacts[0].normal.x < 0)
            {
                ApplyKickForce(new Vector2(-1, 0));
            }

            if (collision.gameObject.tag == "Goomba")
            {
                collision.gameObject.GetComponent<Rigidbody2D>().gravityScale = 3;
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 8, ForceMode2D.Impulse);
                collision.gameObject.GetComponent<Collider2D>().enabled = false;

                Destroy(collision.gameObject, 2);

                ApplyKickForce(new Vector2(-collision.contacts[0].normal.normalized.x, 0));
            }
        }
    }
}
