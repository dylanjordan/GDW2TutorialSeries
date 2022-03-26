using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

	MarioController mario;
	public GameObject projectile;
	public Vector2 velocity;
	bool canShoot = true;
	public Vector2 offset = new Vector2(0.4f, 0.1f);
	public float cooldown = 1f;

	public bool inPowerUp = false;

    private void Start()
    {
		mario = GetComponent<MarioController>();
    }
    // Update is called once per frame
    void Update()
	{
		if (inPowerUp)
        {
			StartCoroutine(PowerUpTimer());
        }

		if (Input.GetKeyDown(KeyCode.Space) && canShoot && mario.runInput > 0 && inPowerUp)
		{

			GameObject go = (GameObject)Instantiate(projectile, (Vector2)transform.position + offset * transform.localScale.x, Quaternion.identity);

			go.GetComponent<Rigidbody2D>().velocity = new Vector2(velocity.x * transform.localScale.x, velocity.y);


			StartCoroutine(CanShoot());
		}
		if (Input.GetKeyDown(KeyCode.Space) && canShoot && mario.runInput < 0 && inPowerUp)
        {
			GameObject go = (GameObject)Instantiate(projectile, (Vector2)transform.position + offset * transform.localScale.x, Quaternion.identity);

			go.GetComponent<Rigidbody2D>().velocity = new Vector2(-velocity.x * transform.localScale.x, velocity.y);


			StartCoroutine(CanShoot());
		}

		
	}


	IEnumerator CanShoot()
	{
		canShoot = false;
		yield return new WaitForSeconds(cooldown);
		canShoot = true;


	}

	private IEnumerator PowerUpTimer()
    {
		yield return new WaitForSeconds(10);
		inPowerUp = false;
    }
}
