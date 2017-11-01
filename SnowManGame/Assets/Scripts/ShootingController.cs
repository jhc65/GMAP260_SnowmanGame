using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingController : MonoBehaviour {

	public Rigidbody rb;
	public float shotSpeed;

	void Start() {
		rb = GetComponent<Rigidbody>();
		rb.velocity = transform.forward * shotSpeed;
	}
}
