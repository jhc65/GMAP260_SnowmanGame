using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamage : MonoBehaviour {

	public int hp = 3;
	public GameObject shotSpawner;
	public GameObject ground;
	private float[] bodyHeights = new float[] {.01f, .55f, .9f}; // Sizes of each snowman boty part

	void OnCollisionEnter(Collision collision) {
		if (collision.collider.tag == "Enemy") {
			hp--;

			// Get rid of enemy
			collision.collider.gameObject.SetActive(false);

			// Lower snowman pos
			float playerY = transform.position.y - bodyHeights[hp];
			float groundY = ground.transform.position.y - bodyHeights[hp];
			transform.position = new Vector3(transform.position.x, playerY, transform.position.z);
			ground.transform.position = new Vector3(ground.transform.position.x, groundY, ground.transform.position.z);
			//shotSpawner.transform.position = new Vector3(shotSpawner.transform.position.x, ((shotSpawner.transform.position.y + bodyHeights[hp]) / 2), shotSpawner.transform.position.z);
			if (hp <= 0) { // dead, so stop spawning and "destroy" player
				GameObject.FindGameObjectWithTag("Respawn").GetComponent<EnemySpawner>().Disable();
				gameObject.SetActive(false);
			}
		}
	}
}
