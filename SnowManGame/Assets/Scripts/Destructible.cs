using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour {

	public GameObject destroyedVersion;

	//get snowball-impact-wood.wav
	private AudioSource audio;
	public AudioClip snowballImpactWood;


	// Use this for initialization
	void Start () {
		audio = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	// If a snowball hits a tree, spawn the destroyed one and remove the old one
	void OnCollisionEnter(Collision col) {
		if (col.gameObject.CompareTag("Projectile")) {
			Instantiate(destroyedVersion, transform.position, transform.rotation);
			Destroy(gameObject);
			Destroy(destroyedVersion.GetComponent<Rigidbody>());

			PlaySound (snowballImpactWood, transform.position);

		}
	}

	void PlaySound(AudioClip clip, Vector3 location){
		AudioSource.PlayClipAtPoint (clip, location);
	}
}
