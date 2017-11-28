using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveObjectAfterTime : MonoBehaviour {

	public int time; // seconds

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Destroy(gameObject, time);

	}
}
