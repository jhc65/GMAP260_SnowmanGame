using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootController : MonoBehaviour {

	public GameObject snowball;
	public float fireRate;

	private static int numBullets = 20;
	private GameObject[] bullets = new GameObject[numBullets];
	private float nextFire = 0;
	private int nextBullet;
	private float bulletSpeed = 20f;

	private bool canShoot = true;


	void Start () {

		// Instantiate projectiles
		for (int i = 0; i < bullets.Length; i++) {
			bullets[i] = (GameObject)Instantiate(snowball);
			bullets[i].SetActive(false);
		}
		nextBullet = 0;
	}

	// Update is called once per frame
	void Update () {
		if ((Input.GetKey(KeyCode.Space) || (Input.GetMouseButton(0))) && Time.time > nextFire && canShoot) {
			nextFire = Time.time + fireRate;
			GameObject bullet = bullets[nextBullet++];
			if (nextBullet >= bullets.Length) {
				nextBullet = 0;
			}

			bullet.SetActive(true);
			bullet.transform.position = transform.position;
			bullet.transform.rotation = transform.rotation;
			bullet.GetComponent<Rigidbody>().velocity = transform.forward * bulletSpeed;
		}

	}

	public void DisableShooting() {
		canShoot = false;
	}

	public void EnableShooting() {
		canShoot = true;
	}
}
