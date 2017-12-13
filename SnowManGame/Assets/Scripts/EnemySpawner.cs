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
    public GameObject snowman;
    public GameObject[] spawnPoints;
	public int[] numberPerRound; // spawn this many in wave
	public float[] frequencies; // increase frequency per wave
	public int numBunniesInGroup = 5;

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

    public Vector3 GetClosestSpawnPoint(Vector3 snowmanPosition)
    {
        int point = 0;
        float minDist = Mathf.Infinity;
        for(int i = 0; i < spawnPoints.Length; i++)
        {
            float dist = Vector3.Distance(spawnPoints[i].transform.position, snowmanPosition);
            if (dist < minDist)
            {
                point = i;
                minDist = dist;
            }
        }

        return spawnPoints[point].transform.position;
    }

    void Spawn() {
        Vector3 snowmanPosition = snowman.transform.position;
        Vector3 spawnPoint = GetClosestSpawnPoint(snowmanPosition);

		for (int j = 0; j < numBunniesInGroup; j++)
        {
			GameObject spawnedEnemy = GameObject.Instantiate(enemy);
			spawnedEnemy.transform.position = new Vector3(spawnPoint.x + Random.Range(0f, 15f) + j * 10, spawnPoint.y, spawnPoint.z + Random.Range(0f, 15f));

		}

		for (int j = 0; j < numBunniesInGroup; j++)
        {
            GameObject spawnedEnemy = GameObject.Instantiate(enemy);
			spawnedEnemy.transform.position = new Vector3(snowmanPosition.x + Random.Range(0f, 15f) + j * 10, snowmanPosition.y, snowmanPosition.z + Random.Range(0f, 15f));
            numSpawnedThisRound++;
        }



        spawnCooldown = frequencies[currentRound];

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
