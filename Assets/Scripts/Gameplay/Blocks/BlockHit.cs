using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockHit : MonoBehaviour
{
    [SerializeField] GameObject blockItem;
    
    [SerializeField] Sprite usedBlock;
    [SerializeField] Sprite unusedBlock;

    SpriteRenderer spriteRenderer;

    bool _blockHit = false;
    bool _blockHitActionPerformed = false;

    GameObject item = null;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!_blockHit)
        {
            spriteRenderer.sprite = unusedBlock;
        }
        else
        {
            spriteRenderer.sprite = usedBlock;

            if (!_blockHitActionPerformed)
            {
                BlockHitAction();
            }
        }
    }

    private void BlockHitAction()
    {
        if (blockItem.CompareTag("Coin"))
        {
            item = Instantiate(blockItem, transform.position + new Vector3(0, 1, 0), Quaternion.Euler(0, 0, 0));
            Destroy(item, 0.5f);
            item.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 5, ForceMode2D.Impulse);

            FindObjectOfType<CoinCounter>().AddCoin(1);
            FindObjectOfType<ScoreCounter>().AddScore(100);

            FindObjectOfType<AudioManager>().Play("Coin");
        }
        else if (blockItem.CompareTag("Powerup"))
        {
            item = Instantiate(blockItem, transform.position + new Vector3(0, 1.01f, 0), Quaternion.Euler(0, 0, 0));
            item.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 5, ForceMode2D.Impulse);

            FindObjectOfType<AudioManager>().Play("MushroomSpawn");
        }

        _blockHitActionPerformed = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (collision.contacts[0].normal.y > 0.5)
            {
                _blockHit = true;
                FindObjectOfType<AudioManager>().Play("Bump");
            }
        }
    }
}
