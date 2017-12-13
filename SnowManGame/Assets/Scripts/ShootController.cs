using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootController : MonoBehaviour {

	public GameObject snowball;
    public GameObject bowlingSnowBall;
    public GameObject snowBomb;
    public float specialFireRate;
	public float fireRate;
	public float bulletSpeed = 20f;

	private static int numBullets = 20;
	private GameObject[] bullets = new GameObject[numBullets];
    private float totalLiveTimeInSeconds = 7.0f;
    private float nextFire = 0;
    private float nextSpecialFire = 0;
	private int nextBullet;
    private int nextSpecialBullet;
    private int nextBombBullet;
    private int armo;
    private GameObject refillzone;

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
        //for (int i = 0; i < specialBullets.Length; i++) {
        //    specialBullets[i] = (GameObject)Instantiate(bowlingSnowBall);
        //    specialBullets[i].SetActive(false);
        //}
        nextBullet = 0;
        nextSpecialBullet = 0;

		audio = GetComponent<AudioSource> ();
	}

	// Update is called once per frame
	void Update () {
		if (!canShoot)
			return;
		
		if ((Input.GetKey (KeyCode.Space) || (Input.GetMouseButton (0))) && Time.time >= nextFire) {
			nextFire = Time.time + fireRate;
			GameObject bullet = bullets[nextBullet++];
			if (nextBullet >= bullets.Length) {
				nextBullet = 0;
			}

			//AudioSource.PlayClipAtPoint (snowballThrowSound, transform.position);
			PlaySound (snowballThrowSound, transform.position);

			bullet.SetActive(true);
			bullet.transform.position = transform.position;
			bullet.transform.rotation = transform.rotation;
			bullet.GetComponent<Rigidbody>().velocity = transform.forward * bulletSpeed;
		}

        if (Input.GetKey(KeyCode.T) && Time.time >= nextSpecialFire) {
            nextSpecialFire = Time.time + specialFireRate;
            GameObject bowlingBullet = (GameObject)Instantiate(bowlingSnowBall);

            PlaySound(snowballThrowSound, transform.position);
            
            bowlingBullet.transform.position = transform.position;
            bowlingBullet.transform.rotation = transform.rotation;
            bowlingBullet.GetComponent<Rigidbody>().velocity = transform.forward * bulletSpeed;
        }

        if (Input.GetKey(KeyCode.F) && Time.time >= nextSpecialFire) {
            nextSpecialFire = Time.time + specialFireRate;

            GameObject snowbomb = (GameObject)Instantiate(snowBomb);

            PlaySound(snowballThrowSound, transform.position);

			snowbomb.transform.position = new Vector3(transform.position.x + 0f, transform.position.y + 3f, transform.position.z + 3f);
            snowbomb.transform.rotation = transform.rotation;
            snowbomb.GetComponent<Rigidbody>().velocity = transform.forward * bulletSpeed;
        }

  //      Debug.DrawRay (transform.position, transform.TransformDirection (Vector3.down),Color.blue, 3f);
		//if (Physics.Raycast (transform.position, transform.TransformDirection (Vector3.down), 3f, refillzone.value) && armo == 0) {
		//	print ("refill!!!!!!!!!!!");
		//	armo = 10;
		//	EnableShooting ();
		//}

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
