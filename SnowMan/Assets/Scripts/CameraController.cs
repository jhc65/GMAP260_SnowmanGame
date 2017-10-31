using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	public GameObject player;       //Public variable to store a reference to the player game object


	public Vector3 offset;     
	public bool lookAt = true;
	public Transform target;
	public Space offsetPositionSpace = Space.Self;

	// Use this for initialization
	void Start () 
	{
		//Calculate and store the offset value by getting the distance between the player's position and camera's position.
		//offset = transform.rotation - player.transform.rotation;
		offset = transform.position - player.transform.position;
	}

	// LateUpdate is called after Update each frame
	void LateUpdate () 
	{
		// Set the position of the camera's transform to be the same as the player's, but offset by the calculated offset distance.
		if (offsetPositionSpace == Space.Self)
		{
			transform.position = target.TransformPoint(offset);
		}
		else
		{
			transform.position = target.position + offset;
		}

//		if(lookAt)
//		{
//			transform.LookAt(player.transform);
//		}
//		else
//		{
//			transform.rotation = player.transform.rotation;
//		}



		// compute rotation
		if (lookAt)
		{
			transform.LookAt(target);
		}
		else
		{
			transform.rotation = target.rotation;
		}

	}
}

