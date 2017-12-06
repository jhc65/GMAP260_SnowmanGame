using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveTowardsPlayer : MonoBehaviour {

	private Transform target;
    public float speed = 5f;
	private int hp = 1;
	private NavMeshAgent agent;
	private Color hitColor = new Color(255f,0f,0f);
		
	// get audio src snowball-impact-rabbit.wav
	public AudioClip snowballImpactBunnySound;
	private AudioSource audio;

    void Start () {
		target = GameObject.FindGameObjectWithTag("Player").transform;
		agent = GetComponent<NavMeshAgent>();

		// init audio
		audio = GetComponent<AudioSource> ();
    }

	void Update () {


		// Head towards player along nav mesh
		agent.destination = target.position; 


		// Head in a straight line towards player
		//Vector3 targetPosition = target.position;
		//transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
    }

	void OnCollisionEnter(Collision collision) {
		if (collision.collider.tag == "Projectile") {
			
				
		}
	}

	void PlaySound(AudioClip clip, Vector3 location){
		AudioSource.PlayClipAtPoint (clip, location);
	}
}
