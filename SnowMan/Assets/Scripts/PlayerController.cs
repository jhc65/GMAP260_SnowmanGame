using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public Rigidbody rb;
	public float rotateSpeed;
	public float speed;
	// Use this for initialization

	public GameObject shot;
	public Transform shotSpawn;
	public float fireRate;

	private float nextFire = 0;


	void Start () {
		rb = GetComponent<Rigidbody>();

	}

	// Update is called once per frame
	void Update () {
		//if (Input.GetKey(KeyCode.LeftArrow)){
		//	transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);

		//}

		//if (Input.GetKey(KeyCode.RightArrow)){
		//	transform.Rotate(-Vector3.up * rotateSpeed * Time.deltaTime);

		//}
		if (Input.GetKey(KeyCode.Space) && Time.time > nextFire)
		{
			nextFire = Time.time + fireRate;
			Instantiate(shot, shotSpawn.position, shotSpawn.rotation);

		}


		if (Input.GetKey (KeyCode.W) || Input.GetKey (KeyCode.UpArrow)) {
			transform.position += transform.forward * Time.deltaTime*speed;
		} 
		else if (Input.GetKey (KeyCode.S) || Input.GetKey (KeyCode.DownArrow)) {
			transform.position -= transform.forward * Time.deltaTime*speed;
		} 
		if (Input.GetKey (KeyCode.A) || Input.GetKey (KeyCode.LeftArrow)) {
			transform.Rotate (0,  -rotateSpeed * Time.deltaTime, 0);
		} 
		else if (Input.GetKey (KeyCode.D) || Input.GetKey (KeyCode.RightArrow)) {
			transform.Rotate (0,  rotateSpeed * Time.deltaTime, 0);
		}
			//transform.Rotate(0.0f,Time.deltaTime*-rotateSpeed, 0,0f);
	}

	void FixedUpdate()  // called each physics steps
	{

//		float moveHorizontal = Input.GetAxis ("Horizontal"); // default axis : Horizontal, vertical
//		float moveVertical = Input.GetAxis ("Vertical");
//		Vector3 movement = new Vector3 (0.0f, 0.0f, moveVertical);
		//rb.velocity = movement*speed;


	}
}
