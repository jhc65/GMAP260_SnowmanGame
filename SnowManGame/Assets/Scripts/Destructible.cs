using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour {

	public GameObject destroyedVersion;
	public AudioClip onHitSound;
	public bool explosionEnabled = true;

	public float explosionRadius = 10f;
	public float explosionPower = 500f;
	public float explosionUpwardsForce = 10f;

	private AudioSource audio;



	// Use this for initialization
	void Start () {
		audio = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	// Create an explosion at position pos
	void TriggerExplosion(Vector3 pos) {
		Collider[] colliders = Physics.OverlapSphere(pos, explosionRadius);
		foreach (Collider hit in colliders) {
			Rigidbody rb = hit.GetComponent<Rigidbody>();
			if (rb != null && rb.gameObject.CompareTag("Enemy")) {
				rb.AddExplosionForce(explosionPower, pos, explosionRadius, explosionUpwardsForce);
			}
		}

		foreach (Rigidbody rb in destroyedVersion.GetComponentsInChildren<Rigidbody>())
			rb.detectCollisions = false;
	}


	// If a snowball hits a tree, spawn the destroyed one and remove the old one
	void OnCollisionEnter(Collision col) {
		if (col.gameObject.CompareTag("Projectile")) {
			Instantiate(destroyedVersion, transform.position, transform.rotation);
			Destroy(gameObject);
			if (explosionEnabled) {
				TriggerExplosion(destroyedVersion.transform.position);
			}

			PlaySound (onHitSound, transform.position);
			GameObject.FindGameObjectWithTag("HitBox").GetComponent<SnowmanHealth>().IncrementBunniesKilled();

		}
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Projectile")) {
            Instantiate(destroyedVersion, transform.position, transform.rotation);
            Destroy(gameObject);
            if (explosionEnabled) {
                TriggerExplosion(destroyedVersion.transform.position);
            }

            PlaySound(onHitSound, transform.position);
			GameObject.FindGameObjectWithTag("HitBox").GetComponent<SnowmanHealth>().IncrementBunniesKilled();

        }
    }

    void PlaySound(AudioClip clip, Vector3 location){
		AudioSource.PlayClipAtPoint (clip, location);
	}
}
