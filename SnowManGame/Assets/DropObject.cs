using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropObject : MonoBehaviour {

	public GameObject obj;
	public bool enabled = true;
	public float fireRate = 1f;
	public float fromHeight = 10f;
	private float nextFire = 0f;


	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (enabled && Input.GetMouseButton(1) && Time.time > nextFire) {
			nextFire = Time.time + fireRate;

			GameObject spawnedObj = GameObject.Instantiate(obj);
			spawnedObj.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + fromHeight, gameObject.transform.position.z);

		}
	}

	void Enable() {
		enabled = true;
	}

	void Disable() {
		enabled = false;
	}
}
