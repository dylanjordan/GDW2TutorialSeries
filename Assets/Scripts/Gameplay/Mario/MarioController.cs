using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarioController : MonoBehaviour
{
    Weapon weapon;

    [SerializeField] float _runForce;
    [SerializeField] float _jumpForce;
    [SerializeField] float _maxSpeed;

    [SerializeField] GameObject bigMarioPrefab;
    [SerializeField] GameObject smollMarioPrefab;

    Transform _trans;
    Rigidbody2D _body;

    public float runInput;

    bool jumpInput;
    bool isGrounded;
    bool isBig;

    // Start is called before the first frame update
    void Start()
    {
        weapon = GetComponent<Weapon>();
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

    void EnemyBounce()
    {
        _body.AddForce(Vector2.up * _jumpForce / 1.5f, ForceMode2D.Impulse);
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

        if (collision.gameObject.tag == "Goomba")
        {
            if (collision.contacts[0].normal.y > 0.5)
            {
                EnemyBounce();
                collision.gameObject.GetComponent<Goomba>().SetIsSquashed(true);
            }
            else
            {
                if (!collision.gameObject.GetComponent<Goomba>().GetIsSquashed())
                {
                    if (isBig)
                    {
                        GetComponent<BoxCollider2D>().size = smollMarioPrefab.GetComponent<BoxCollider2D>().size;
                        isBig = false;
                    }
                    else
                    {
                        Debug.Log("I died");
                    }
                    
                }
            }
        }

        if (collision.gameObject.tag == "Koopa")
        {
            if (collision.contacts[0].normal.y > 0.5 && !collision.gameObject.GetComponent<Koopa>().GetIsSquashed())
            {
                EnemyBounce();

                collision.gameObject.GetComponent<Koopa>().SetIsSquashed(true);
                collision.gameObject.GetComponent<Koopa>().SetIsMoving(false);

                collision.gameObject.GetComponent<BoxCollider2D>().size = new Vector2(1, 1);
            }
            else if (collision.gameObject.GetComponent<Koopa>().GetIsSquashed() && !collision.gameObject.GetComponent<Koopa>().GetIsKicked())
            {
                if (collision.gameObject.transform.position.x > _trans.position.x)
                {
                    collision.gameObject.GetComponent<Koopa>().ApplyKickForce(new Vector2(1, 0));
                }
                if (collision.gameObject.transform.position.x < _trans.position.x)
                {
                    collision.gameObject.GetComponent<Koopa>().ApplyKickForce(new Vector2(-1, 0));
                }
            }
            else if (collision.contacts[0].normal.y > 0.5 && collision.gameObject.GetComponent<Koopa>().GetIsKicked())
            {
                EnemyBounce();

                collision.gameObject.GetComponent<Koopa>().SetIsKicked(false);
                collision.gameObject.GetComponent<Koopa>().SetIsMoving(false);

                collision.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, collision.gameObject.GetComponent<Rigidbody2D>().velocity.y);
            }
            else
            {
                if (collision.gameObject.GetComponent<Koopa>().GetIsMoving())
                {
                    if (isBig)
                    {
                        GetComponent<BoxCollider2D>().size = smollMarioPrefab.GetComponent<BoxCollider2D>().size;
                        isBig = false;
                    }
                    else
                    {
                        Debug.Log("I died");
                    }
                }
            }
        }
        if (collision.gameObject.tag == "Piranha Plant")
        {
            if (isBig)
            {
                GetComponent<BoxCollider2D>().size = smollMarioPrefab.GetComponent<BoxCollider2D>().size;
                isBig = false;
            }
            else
            {
                Debug.Log("I died");
            }
        }

        if (collision.gameObject.name.Contains("Mushroom"))
        {
            if (!isBig)
            {
                Destroy(collision.gameObject);

                isBig = true;

                GetComponent<BoxCollider2D>().size = bigMarioPrefab.GetComponent<BoxCollider2D>().size;
            }
            else
            {
                Destroy(collision.gameObject);
            }
        }

        if (collision.gameObject.name.Contains("FirePlant"))
        {
            Destroy(collision.gameObject);

            weapon.inPowerUp = true;
        }


    }
}
