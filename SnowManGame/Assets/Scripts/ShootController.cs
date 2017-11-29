using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootController : MonoBehaviour {

	public GameObject snowball;
	public float fireRate;
	public float bulletSpeed = 20f;
	public LayerMask refillzone;

	private static int numBullets = 20;
	private GameObject[] bullets = new GameObject[numBullets];
	private float nextFire = 0;
	private int nextBullet;
	private int armo = 10;

	private bool canShoot = true;

	// get snowball-throw.wav
	public AudioClip snowballThrowSound;
	private AudioSource audio;

	void Start () {

		// Instantiate projectiles
		for (int i = 0; i < bullets.Length; i++) {
			bullets[i] = (GameObject)Instantiate(snowball);
			bullets[i].SetActive(false);
		}
		nextBullet = 0;

		audio = GetComponent<AudioSource> ();
	}

	// Update is called once per frame
	void Update () {
		if ((Input.GetKey (KeyCode.Space) || (Input.GetMouseButton (0))) && Time.time > nextFire && canShoot) {
			nextFire = Time.time + fireRate;
			GameObject bullet = bullets [nextBullet++];
			if (nextBullet >= bullets.Length) {
				nextBullet = 0;
			}

			//AudioSource.PlayClipAtPoint (snowballThrowSound, transform.position);
			PlaySound (snowballThrowSound, transform.position);

			bullet.SetActive (true);
			bullet.transform.position = transform.position;
			bullet.transform.rotation = transform.rotation;
			bullet.GetComponent<Rigidbody> ().velocity = transform.forward * bulletSpeed;

			armo--;
			if (armo <= 0) {
				DisableShooting ();
			}
		}

		Debug.DrawRay (transform.position, transform.TransformDirection (Vector3.down),Color.blue, 3f);
		if (Physics.Raycast (transform.position, transform.TransformDirection (Vector3.down), 3f, refillzone.value) && armo == 0) {
			print ("refill!!!!!!!!!!!");
			armo = 10;
			EnableShooting ();
		}

	}

	public void DisableShooting() {
		canShoot = false;
	}

	public void EnableShooting() {
		canShoot = true;
	}

	public float GetBulletLaunchSpeed() {
		return bulletSpeed;
	}

	public void SetBulletLaunchSpeed(float s) {
		bulletSpeed = s;
	}

	void PlaySound(AudioClip clip, Vector3 location){
		AudioSource.PlayClipAtPoint (clip, location);
	}
}
