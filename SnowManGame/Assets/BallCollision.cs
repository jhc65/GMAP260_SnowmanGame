using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCollision : MonoBehaviour {

	public GameObject collisionEffect;
	//public float collisionShrinkFactor = 2f;

	//private Vector3 initialScale;
	//private float slowDownOnImpact = 3f;

	// get audio src snowball-impact-snow.wav
	private AudioSource audio;
	public AudioClip snowballImpactGroundSound;


	// Use this for initialization
	void Start () {
		//initialScale = transform.localScale;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter(Collision collision) {
		if (collision.gameObject.tag == "Ground") {
			Vector3 landingPos = transform.position;
			landingPos.y += .5f; // Show a bit more above ground
			GameObject spawnedParticlEffect = GameObject.Instantiate(collisionEffect, landingPos, Quaternion.identity);
			spawnedParticlEffect.SetActive(true);

			PlaySound (snowballImpactGroundSound, transform.position);

			// to shrink snowball
			//transform.localScale = new Vector3(initialScale.x / collisionShrinkFactor, initialScale.y / collisionShrinkFactor, initialScale.z / collisionShrinkFactor);
			Vector3 vel = transform.GetComponent<Rigidbody>().velocity;

			//if you want to slow down snowball and not destroy it
			//transform.GetComponent<Rigidbody>().velocity = new Vector3(vel.x, vel.y, vel.z / slowDownOnImpact);
			transform.gameObject.SetActive(false);

		}// else if(collision.gameObject.tag == "Tree")
			// play snowball hit tree sound
	}

	void PlaySound(AudioClip clip, Vector3 vector){
		AudioSource.PlayClipAtPoint (clip, vector);
	}
}
