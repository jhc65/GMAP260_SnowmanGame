using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTowardsPlayer : MonoBehaviour {

	private Transform target;
    public float speed = 5f;
	private int hp = 1;
	private Color hitColor = new Color(0f,0f,0f);
		
    void Start () {
		target = GameObject.FindGameObjectWithTag("Player").transform;
    }

	void Update () {
		/*
		var newRotation = new Vector3(transform.eulerAngles.x, target.eulerAngles.y, transform.eulerAngles.z);
		transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(newRotation), .5f * Time.fixedDeltaTime);
		Quaternion targetRotation = transform.rotation;
		*/

		// Head in a straight line towards player
		Vector3 targetPosition = target.position;
		transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
    }

	void OnCollisionEnter(Collision collision) {
		if (collision.collider.tag == "Projectile") {
			
			// "Remove" bullet
			collision.collider.gameObject.SetActive(false);

			hp--;
			//GetComponent<MeshRenderer>().material.color = hitColor; // Effect to show enemy was hit. Change color to white
			if (hp <= 0)
				Destroy(gameObject);
				
		}
	}
}
