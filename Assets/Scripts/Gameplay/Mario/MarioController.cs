using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarioController : MonoBehaviour
{
    [SerializeField] float _runForce;
    [SerializeField] float _jumpForce;
    [SerializeField] float _maxSpeed;

    Transform _trans;
    Rigidbody2D _body;

    float runInput;

    bool jumpInput;
    bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        _trans = GetComponent<Transform>();
        _body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        runInput = Input.GetAxis("Horizontal");
        
        if (Input.GetKey(KeyCode.W))
        {
            jumpInput = true;
        }
        else
        {
            jumpInput = false;
        }

        if (runInput == 0 && _body.velocity.y == 0)
        {
            _body.drag = 3;
        }
        else
        {
            _body.drag = 1;
        }
    }

    private void FixedUpdate()
    {
        if (runInput != 0)
        {
            Run();
        }

        if (jumpInput && isGrounded)
        {
            Jump();
        }
    }

    void Run()
    {
        if (Mathf.Abs(_body.velocity.x) >= _maxSpeed)
        {
            return;
        }
        if (runInput > 0)
        {
            _body.AddForce(Vector2.right * _runForce, ForceMode2D.Force);

            _trans.rotation = Quaternion.Euler(0, 180, 0);
        }
        if (runInput < 0)
        {
            _body.AddForce(Vector2.left * _runForce, ForceMode2D.Force);

            _trans.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    void Jump()
    {
        _body.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
        isGrounded = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        for (int i = 0; i < collision.contacts.Length; i++)
        {
            if (collision.contacts[i].normal.y > 0.5)
            {
                isGrounded = true;
            }
        }
    }
}
