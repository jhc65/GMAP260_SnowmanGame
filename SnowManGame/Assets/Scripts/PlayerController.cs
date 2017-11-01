using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float rotateSpeed;
	public float speed;
	public GameObject snowball;
	public Transform shotSpawn;
	public float fireRate;

	private static int numBullets = 30;
	private GameObject[] bullets = new GameObject[numBullets];
	private float nextFire = 0;
	private int nextBullet;

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
		if (Input.GetKey(KeyCode.Space) && Time.time > nextFire && canShoot) {
			nextFire = Time.time + fireRate;
			GameObject bullet = bullets[nextBullet++];
			if (nextBullet >= bullets.Length) {
				nextBullet = 0;
			}

			bullet.SetActive(true);
			bullet.transform.position = shotSpawn.position;
			bullet.transform.rotation = shotSpawn.rotation;
		}

		if (Input.GetAxis("Vertical") > 0) {
			transform.position += transform.forward * Time.deltaTime*speed;
		} 
		if (Input.GetAxis("Vertical") < 0) {
			transform.position -= transform.forward * Time.deltaTime*speed;
		} 
		if (Input.GetAxis("Horizontal") < 0) {
			transform.Rotate (0,  -rotateSpeed, 0);
		} 
		if (Input.GetAxis("Horizontal") > 0) {
			transform.Rotate (0,  rotateSpeed, 0);
		}
	}

	public void DisableShooting() {
		canShoot = false;
	}

	public void EnableShooting() {
		canShoot = true;
	}
}
