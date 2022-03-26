using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public Rigidbody2D _rb;
    public LayerMask enemy;
    public Vector2 _velocity;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, 8);
        _rb = GetComponent<Rigidbody2D>();
        _velocity = _rb.velocity;
    }

    // Update is called once per frame
    void Update()
    {
        if (_rb.velocity.y < _velocity.y)
        {
            _rb.velocity = _velocity;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _rb.velocity = new Vector2(_velocity.x, -_velocity.y);

        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            Destroy(collision.gameObject);
            Destroy(this.gameObject);
        }
        if (Mathf.Abs(collision.contacts[0].normal.x) > 0.4f)
        {
            Destroy(this.gameObject);
        }
    }
}
