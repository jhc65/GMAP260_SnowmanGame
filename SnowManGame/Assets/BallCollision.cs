using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCollision : MonoBehaviour {

	public GameObject collisionEffect;
	public GameObject collisionEffectBlood;
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
		Vector3 landingPos = transform.position;
		landingPos.y += .5f; 
		GameObject spawnedParticlEffect = GameObject.Instantiate(collisionEffect, landingPos, Quaternion.identity);
		spawnedParticlEffect.SetActive(true);
		transform.gameObject.SetActive(false);

		if (collision.gameObject.CompareTag("Ground")) {
			PlaySound (snowballImpactGroundSound, transform.position);
		}
		if (collision.gameObject.CompareTag("Enemy")) {
			Vector3 pos = collision.gameObject.transform.position;
			pos.y += 1f;
			GameObject bloodEffect = GameObject.Instantiate(collisionEffectBlood, pos, Quaternion.identity);
			bloodEffect.SetActive(true);

		}
			
		//}// else if(collision.gameObject.tag == "Tree")
			// play snowball hit tree sound
	}

	void PlaySound(AudioClip clip, Vector3 location){
		AudioSource.PlayClipAtPoint (clip, location);
	}
}
