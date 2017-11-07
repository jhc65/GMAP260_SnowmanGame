using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float rotateSpeed = 2.0f;
	public float MoveSpeed = 10.0f;
	public GameObject snowball;
	public Transform shotSpawn;
	public float fireRate;

	private static int numBullets = 20;
	private GameObject[] bullets = new GameObject[numBullets];
	private float nextFire = 0;
	private int nextBullet;
	private float bulletSpeed = 20f;

	private bool canShoot = true;
	private int hp = 3;
	private float angleH = 0;                                          // Float to store camera horizontal angle related to mouse movement.
	private float angleV = 0;                                          // Float to store camera horizontal angle related to mouse movement.
	public float maxVerticalAngle = 10f;                               // Camera max clamp angle. 
	public float minVerticalAngle = -20f;                              // Camera min clamp angle.
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

		Vector3 rot = transform.localRotation.eulerAngles;
		Cursor.visible = false;

	}

	// Update is called once per frame
	void Update () {
		if ((Input.GetKey(KeyCode.Space) || (Input.GetMouseButtonDown(0))) && Time.time > nextFire && canShoot) {
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
			
		// Slide snowman around
		transform.Translate (MoveSpeed*Input.GetAxis("Horizontal")*Time.deltaTime, 0f, MoveSpeed*Input.GetAxis("Vertical")*Time.deltaTime);
	
		// Mouse movement
		angleH += Mathf.Clamp(Input.GetAxis("Mouse X"), -1, 1) * rotateSpeed;
		angleH += Mathf.Clamp(Input.GetAxis("Mouse Y"), -1, 1) * rotateSpeed;

		// Set vertical movement limit.
		angleV = Mathf.Clamp(angleV, minVerticalAngle, maxVerticalAngle);

		// Set camera orientation.
		Quaternion camYRotation = Quaternion.Euler(0, angleH, 0);
		Quaternion aimRotation = Quaternion.Euler(0f, angleH, 0);
		transform.rotation = aimRotation;
	}

	public void DisableShooting() {
		canShoot = false;
	}

	public void EnableShooting() {
		canShoot = true;
	}
}
