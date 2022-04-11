using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    bool isDead;
    bool isRunning;
    bool deathStarted;

    // Start is called before the first frame update
    void Start()
    {
        weapon = GetComponent<Weapon>();
        _trans = GetComponent<Transform>();
        _body = GetComponent<Rigidbody2D>();

        FindObjectOfType<AudioManager>().Play("Music");
    }

    // Update is called once per frame
    void Update()
    {
        runInput = Input.GetAxis("Horizontal");

        if (runInput == 0)
        {
            isRunning = false;
        }
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

        if (_trans.position.y <= -5 && !deathStarted)
        {
            StartDeath();
        }

        if (_trans.position.y <= -7)
        {
            Die();
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
        isRunning = true;

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

        FindObjectOfType<AudioManager>().Play("Jump");
    }

    void EnemyBounce()
    {
        _body.AddForce(Vector2.up * _jumpForce / 1.5f, ForceMode2D.Impulse);
        isGrounded = false;
    }

    void StartDeath()
    {
        FindObjectOfType<Lives>().LoseLife();
        isDead = true;

        FindObjectOfType<AudioManager>().Stop("Music");

        if (FindObjectOfType<Lives>().GetCurrentLives() < 1)
        {
            FindObjectOfType<AudioManager>().Play("GameOver");
        }
        else
        {
            FindObjectOfType<AudioManager>().Play("LifeLost");
        }

    

        _body.velocity = Vector2.zero;

        _body.gravityScale = 3;

        _body.AddForce(Vector3.up * _jumpForce / 2, ForceMode2D.Impulse);

        GetComponent<Collider2D>().enabled = false;

        deathStarted = true;
    }

    void Die()
    {
        if (FindObjectOfType<Lives>().GetCurrentLives() < 1)
        {
            FindObjectOfType<LevelStatus>().SetGameOver(true);
        }
        else
        {
            FindObjectOfType<LevelStatus>().SetLevelFailed(true);
        }
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
                FindObjectOfType<ScoreCounter>().AddScore(1000);

                FindObjectOfType<AudioManager>().Play("Bump");
            }
            else
            {
                if (!collision.gameObject.GetComponent<Goomba>().GetIsSquashed())
                {
                    if (isBig)
                    {
                        GetComponent<BoxCollider2D>().size = smollMarioPrefab.GetComponent<BoxCollider2D>().size;
                        isBig = false;

                        FindObjectOfType<AudioManager>().Play("PowerDown");
                    }
                    else
                    {
                        StartDeath();
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

                FindObjectOfType<ScoreCounter>().AddScore(1000);

                FindObjectOfType<AudioManager>().Play("Bump");
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
                FindObjectOfType<ScoreCounter>().AddScore(100);
            }
            else
            {
                if (collision.gameObject.GetComponent<Koopa>().GetIsMoving())
                {
                    if (isBig)
                    {
                        GetComponent<BoxCollider2D>().size = smollMarioPrefab.GetComponent<BoxCollider2D>().size;
                        isBig = false;

                        FindObjectOfType<AudioManager>().Play("PowerDown");
                    }
                    else
                    {
                        StartDeath();
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

                FindObjectOfType<AudioManager>().Play("PowerDown");
            }
            else
            {
                StartDeath();
            }
        }

        if (collision.gameObject.name.Contains("Mushroom"))
        {
            FindObjectOfType<AudioManager>().Play("PowerUp");
            if (!isBig)
            {
                Destroy(collision.gameObject);

                isBig = true;

                GetComponent<BoxCollider2D>().size = bigMarioPrefab.GetComponent<BoxCollider2D>().size;
            }
            else
            {
                Destroy(collision.gameObject);

                FindObjectOfType<ScoreCounter>().AddScore(500);
            }
        }

        if (collision.gameObject.name.Contains("FirePlant"))
        {
            Destroy(collision.gameObject);

            weapon.inPowerUp = true;
        }


    }

    public bool GetIsRunning()
    {
        return isRunning;
    }

    public bool GetIsGrounded()
    {
        return isGrounded;
    }

    public bool GetIsDead()
    {
        return isDead;
    }

    public bool GetIsBig()
    {
        return isBig;
    }
}
