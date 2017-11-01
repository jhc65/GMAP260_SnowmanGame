using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float rotateSpeed;
	public float speed;
	public GameObject snowball;
	public Transform shotSpawn;
	public float fireRate;

	private static int numBullets = 20;
	private GameObject[] bullets = new GameObject[numBullets];
	private float nextFire = 0;
	private int nextBullet;
	private float bulletSpeed = 20f;

	private bool canShoot = true;
	private int hp = 1;

	void OnCollisionEnter(Collision collision) {
		if (collision.collider.tag == "Enemy") {
			hp--;
			if (hp <= 0) { // dead, so stop spawning and "destroy" player
				GameObject.FindGameObjectWithTag("Spawner").GetComponent<EnemySpawner>().Disable();
				gameObject.SetActive(false);
			}
		}
	}

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
			bullet.GetComponent<Rigidbody>().velocity = transform.forward * bulletSpeed;
		}

		if (Input.GetAxis("Vertical") > 0) {
			transform.position += transform.forward * Time.deltaTime * speed;
		} 
		if (Input.GetAxis("Vertical") < 0) {
			transform.position -= transform.forward * Time.deltaTime * speed;
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
