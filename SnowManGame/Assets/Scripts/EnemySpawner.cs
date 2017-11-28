using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Debugger Controls
	You can expand the spawnPoints parameter, and change the Size to the number
	  of desired spawn points.  For each point, enter the coordinates for spawn positions
	
	Change the spawnCooldown to the desired time between enemy spawns
*/
public class EnemySpawner : MonoBehaviour {

	public GameObject enemy;
	public GameObject[] spawnPoints;
	public int[] numberPerRound; // spawn this many in wave
	public float[] frequencies; // increase frequency per wave

	private float spawnCooldown = 0.0f;
	private bool isEnabled = true;
	private int currentRound;
	private int numSpawnedThisRound;

	void Start () {
		currentRound = 0;
	}

	// Update spawn points to always be the ones in front of the player
	void UpdateSpawnPoints() {

	}

	void Spawn() {
		int i = Random.Range(0, spawnPoints.Length); // random spawn point
		Vector3 spawnPosition = spawnPoints[i].transform.position;
		GameObject spawnedEnemy = GameObject.Instantiate(enemy);
		spawnedEnemy.transform.position = spawnPosition;
		spawnCooldown = frequencies[currentRound];
		numSpawnedThisRound++;

		// If all bunnies are spawned for the round, begin next round
		if (numSpawnedThisRound == numberPerRound[currentRound] && currentRound < numberPerRound.Length - 1) { 
			currentRound++;
		}
	}

	// Simulate round using array values set above
	void RunRound() {
		if (spawnCooldown <= 0.0f && spawnPoints.Length > 0) {
			Spawn();
		}
		else {
			spawnCooldown -= Time.deltaTime;
		}



	}

	void Update () {
		if (!isEnabled)
			return;

		RunRound();


	}

	public void Enable() {
		isEnabled = true;
	}

	public void Disable() {
		isEnabled = false; 
	}
}
