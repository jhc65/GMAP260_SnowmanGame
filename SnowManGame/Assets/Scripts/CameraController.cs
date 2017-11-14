using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	public GameObject player;       //Public variable to store a reference to the player game object

	public Vector3 OffsetFromPlayer;
	private Vector3 offset;     
	public bool lookAt = true;
	public Transform target;
	public Space offsetPositionSpace = Space.Self;

	void Start () {
		// Disable cursor
		Cursor.visible = false;

		//Calculate and store the offset value by getting the distance between the player's position and camera's position.
		offset = transform.position - player.transform.position;
		offset += OffsetFromPlayer;
	}

	// LateUpdate is called after Update each frame
	void LateUpdate () 
	{

		// Set the position of the camera's transform to be the same as the player's, but offset by the calculated offset distance.
		if (offsetPositionSpace == Space.Self) {
			transform.position = target.TransformPoint(offset);
		}
		else {
			transform.position = target.position + offset;
		}

		// compute rotation
		if (lookAt) {
			transform.LookAt(target);

			// Copy rotation of player
			transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, this.transform.localEulerAngles.y, player.transform.rotation.y);
		//	transform.rotation = up and down with mouse Y
		
		}
		else {
		//	transform.rotation = target.rotation;
		}
	}
}

