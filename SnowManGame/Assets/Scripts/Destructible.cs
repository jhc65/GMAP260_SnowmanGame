using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour {

	public GameObject destroyedVersion;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	// If a snowball hits a tree, spawn the destroyed one and remove the old one
	void OnCollisionEnter(Collision col) {
		if (col.gameObject.CompareTag("Projectile")) {
			Instantiate(destroyedVersion, transform.position, transform.rotation);
			Destroy(gameObject);
		}
	}
}
