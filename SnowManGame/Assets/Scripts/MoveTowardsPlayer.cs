using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTowardsPlayer : MonoBehaviour {

    private Transform target;
    public float speed = 5f;
	private int hp = 2;
	private Color hitColor = new Color(0f,0f,0f);

	public AudioClip enemyHitSound;
	private AudioSource source;
		
    void Start () {
        target = GameObject.FindGameObjectWithTag("Player").transform;

		source = GetComponent<AudioSource>();
    }

	void Update () {
		if (target == null) return;
		Vector3 targetPosition = new Vector3(target.position.x, target.position.y + 1f, target.position.z);
		transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
    }

	void OnCollisionEnter(Collision collision) {
		if (collision.collider.tag == "Projectile") {
			
			// "Remove" bullet
			collision.collider.gameObject.SetActive(false);

			//hit sound
			source.Play();

			hp--;
			GetComponent<MeshRenderer>().material.color = hitColor; // Effect to show enemy was hit. Change color to white
			if (hp <= 0)
				Destroy(gameObject);
				
		}
	}
}
